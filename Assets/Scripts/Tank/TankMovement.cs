using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class TankMovement : MonoBehaviour, IPoolable
    {
        #region Public Variables

        /// <summary>
        /// Our animator
        /// </summary>
        [Header("Setup")] public Animator TankAnimator;

        /// <summary>
        /// The duration for a single movement step
        /// </summary>
        [Header("Movement")] public float MovementDuration = 0.25f;

        /// <summary>
        /// How much distance to move in a single move
        /// </summary>
        public int MovementDistance = 1;

        /// <summary>
        /// Offset for checking valid tiles
        /// </summary>
        public float TileCheckOffset = 1.5f;

        /// <summary>
        /// Tanks width in tiles
        /// </summary>
        public int TankWidthInTiles = 1;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our cached transform 
        /// </summary>
        private Transform mTransform;

        /// <summary>
        /// Our health component
        /// </summary>
        private Health mHealth;

        /// <summary>
        /// Whether we can perform movement
        /// </summary>
        private bool mCanMove;

        /// <summary>
        /// Collidable tilemaps
        /// </summary>
        private List<Tilemap> mCollidableTilemaps;

        /// <summary>
        /// Are we moving
        /// </summary>
        private bool mIsPerformingMovement;

        /// <summary>
        /// Our movement direction
        /// </summary>
        private Vector2 mMovementDirection;

        /// <summary>
        /// Our current rotation
        /// </summary>
        private float mTankRotation;

        /// <summary>
        /// Cached movement animator string
        /// </summary>
        private static readonly int Movement = Animator.StringToHash("Movement");

        /// <summary>
        /// Our current movement duration
        /// </summary>
        private float mCurrentMovementDuration;

        /// <summary>
        /// Routine for boosting our speed
        /// </summary>
        private IEnumerator mBoostedSpeedRoutine;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Cache our transform for efficiency
            mTransform = transform;

            // Grab our health component
            mHealth = GetComponent<Health>();

            // Setup our current movement duration
            mCurrentMovementDuration = MovementDuration;
        }

        private void Start()
        {
            // Initialise this component
            Init();
        }

        private void OnEnable()
        {
            if (mHealth)
            {
                mHealth.OnKilled.AddListener(OnKilled);
            }
        }

        private void OnDisable()
        {
            if (mHealth)
            {
                mHealth.OnKilled.RemoveListener(OnKilled);
            }
        }

        private void FixedUpdate()
        {
            // Check we can move
            if (!mCanMove)
            {
                return;
            }

            // Calculate the tanks movement
            CalculateMovement();

            // Update the animator
            UpdateAnimator();
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// Called when we are spawned
        /// </summary>
        public void OnPoolSpawn()
        {
            // Set we can move again
            mCanMove = true;

            // Ensure we are not boosting our speed
            if (mBoostedSpeedRoutine != null)
            {
                StopCoroutine(mBoostedSpeedRoutine);
                mBoostedSpeedRoutine = null;
            }

            // Set our speed back to default
            mCurrentMovementDuration = MovementDuration;
        }

        /// <summary>
        /// Called when we are de-spawned
        /// </summary>
        public void OnPoolUnSpawn()
        {
        }

        /// <summary>
        /// Set if we can move
        /// </summary>
        /// <param name="canMove"></param>
        public void SetCanMove(bool canMove)
        {
            mCanMove = canMove;
        }

        /// <summary>
        /// Set our input values
        /// </summary>
        /// <param name="inputVector"></param>
        public void SetInput(Vector2 inputVector)
        {
            mMovementDirection = inputVector;
        }

        /// <summary>
        /// Checks to see if we can move forward based on the tiles surrounding us
        /// </summary>
        /// <param name="currentCellPosition"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool CheckCanMoveInDirection(Vector2 currentCellPosition, Vector2 direction)
        {
            bool canMoveForward = true;

            // Check all tiles based on our width
            float currentPos = 0f - (TankWidthInTiles * 0.5f) + 0.5f;
            for (int i = 0; i < TankWidthInTiles; i++)
            {
                // Calculate our target cell position
                Vector2 targetCellPos = currentCellPosition + (TileCheckOffset + MovementDistance) * direction;
                targetCellPos += currentPos * Vector2.Perpendicular(direction);

                // Check to see if we can move to this destination
                if (!CheckCanMoveToSpace(targetCellPos))
                {
                    canMoveForward = false;
                    break;
                }

                // Increase our next check position by one tile
                currentPos += 1f;
            }

            return canMoveForward;
        }

        /// <summary>
        /// Boosts the speed for a specified duration
        /// </summary>
        /// <param name="boostedSpeed"></param>
        /// <param name="duration"></param>
        public void BoostSpeed(float boostedSpeed, float duration)
        {
            // If we cannot move, do not boost speed
            if (!mCanMove)
            {
                return;
            }

            // Start the routine
            mBoostedSpeedRoutine = BoostSpeedRoutine(boostedSpeed, duration);
            StartCoroutine(mBoostedSpeedRoutine);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise this component
        /// </summary>
        private void Init()
        {
            // Set our collidable tilemaps to what our LevelManager stored
            mCollidableTilemaps = LevelManager.Instance.CollidableTilemaps;

            // Set we can move
            mCanMove = true;
        }

        /// <summary>
        /// Calculate our movement
        /// </summary>
        private void CalculateMovement()
        {
            // Check we have valid input
            if (mMovementDirection.normalized.magnitude <= 0f || mIsPerformingMovement)
            {
                return;
            }

            // Update the rotation of the tank
            UpdateRotation();

            // Set our current and target cell positions
            Vector2 currentCellPosition = mTransform.position;

            // Check if we can move to this tile
            if (!CheckCanMoveInDirection(currentCellPosition, mTransform.up))
            {
                // We cannot move here
                return;
            }

            // Move to the target tile
            StartCoroutine(MoveToTile(currentCellPosition + MovementDistance * (Vector2) mTransform.up));
        }

        /// <summary>
        /// Moves the tank to a target tile
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        private IEnumerator MoveToTile(Vector2 targetPosition)
        {
            // We have just started movement
            mIsPerformingMovement = true;

            // First we calculate the remaining distance, squared
            float remainingDistanceSquared = ((Vector2) mTransform.position - targetPosition).sqrMagnitude;

            // Calculate the inverse of our movement time
            float movementTimeInversed = 1 / mCurrentMovementDuration;

            // Whilst we still have some movement to do
            while (remainingDistanceSquared > float.Epsilon)
            {
                // Grab our position for efficiency
                Vector2 ourPosition = mTransform.position;

                // Calculate our new position
                Vector2 newPosition = Vector2.MoveTowards(ourPosition, targetPosition, movementTimeInversed * Time.deltaTime);

                // Update our transform to our new position
                mTransform.position = newPosition;

                // Work out the remaining distance to our destination
                remainingDistanceSquared = (ourPosition - targetPosition).sqrMagnitude;

                // Wait
                yield return null;
            }

            // We have finished movement
            mIsPerformingMovement = false;
        }

        /// <summary>
        /// Updates the tanks rotation
        /// </summary>
        private void UpdateRotation()
        {
            if (mMovementDirection.y > 0)
            {
                mTankRotation = 0f;
            }
            else if (mMovementDirection.y < 0)
            {
                mTankRotation = 180f;
            }
            else if (mMovementDirection.x < 0)
            {
                mTankRotation = 90f;
            }
            else if (mMovementDirection.x > 0)
            {
                mTankRotation = -90f;
            }

            mTransform.localEulerAngles = new Vector3(0f, 0f, mTankRotation);
        }

        /// <summary>
        /// Updates the animator
        /// </summary>
        private void UpdateAnimator()
        {
            if (!TankAnimator)
            {
                return;
            }

            TankAnimator.SetFloat(Movement, mMovementDirection.normalized.magnitude);
        }

        /// <summary>
        /// Checks all of the tilemaps to see if we can move there
        /// </summary>
        /// <param name="targetCellPos"></param>
        /// <returns></returns>
        private bool CheckCanMoveToSpace(Vector2 targetCellPos)
        {
            bool result = true;

            foreach (Tilemap tilemap in mCollidableTilemaps)
            {
                // Check to see if we can move to this destination
                if (TilemapUtils.GetTilemapCellAtPosition(tilemap, targetCellPos))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Called when we have been killed
        /// </summary>
        private void OnKilled()
        {
            // Disable movement if we have been killed
            mCanMove = false;

            if (mBoostedSpeedRoutine != null)
            {
                StopCoroutine(mBoostedSpeedRoutine);
            }
        }

        /// <summary>
        /// Boosts our speed for the specified duration
        /// </summary>
        /// <param name="boostedSpeed"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private IEnumerator BoostSpeedRoutine(float boostedSpeed, float duration)
        {
            // Save our current speed
            float previousSpeed = mCurrentMovementDuration;

            // Set our boosted speed
            mCurrentMovementDuration = boostedSpeed;

            // Wait for our duration
            yield return new WaitForSeconds(duration);

            // Restore our speed
            mCurrentMovementDuration = previousSpeed;

            // Reset our routine
            mBoostedSpeedRoutine = null;
        }

        #endregion
    }
}