using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class TankMovement : MonoBehaviour
    {
        #region Public Variables

        [Header("Setup")] public Animator TankAnimator;

        [Header("Movement")] public float MovementDuration = 0.25f;
        public int MovementSpeed = 1;
        public float TileCheckOffset = 1.5f;
        public int TankWidthInTiles = 1;

        #endregion

        #region Private Variables

        // Components
        private Transform mTransform;

        // Tilemaps
        private List<Tilemap> mCollidableTilemaps;

        // Movement
        private bool mIsPerformingMovement;
        private Vector2 mMovementDirection;

        // Rotation
        private float mTankRotation;

        // Animator
        private static readonly int Movement = Animator.StringToHash("Movement");

        #endregion

        #region Unity Methods

        private void Awake()
        {
            mTransform = transform;
        }

        private void Start()
        {
            Init();
        }

        private void FixedUpdate()
        {
            // Calculate the tanks movement
            CalculateMovement();

            // Update the animator
            UpdateAnimator();
        }

        #endregion

        #region Public Variables

        public void SetInput(Vector2 inputVector)
        {
            mMovementDirection = inputVector;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            mCollidableTilemaps = LevelManager.Instance.CollidableTilemaps;
        }

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

            bool canMoveToTarget = true;

            // Check all tiles based on our width
            float currentPos = 0f - (TankWidthInTiles * 0.5f) + 0.5f;
            for (int i = 0; i < TankWidthInTiles; i++)
            {
                // Calculate our target cell position
                Vector2 targetCellPos = currentCellPosition + (TileCheckOffset + MovementSpeed) * (Vector2) mTransform.up;
                targetCellPos += currentPos * (Vector2) transform.right;

                // Check to see if we can move to this destination
                if (!CheckCanMoveToSpace(targetCellPos))
                {
                    canMoveToTarget = false;
                    break;
                }

                // Increase our next check position by one tile
                currentPos += 1f;
            }

            // Check if we can move to this tile
            if (!canMoveToTarget)
            {
                // We cannot move here
                return;
            }

            // Move to the target tile
            StartCoroutine(MoveToTile(currentCellPosition + MovementSpeed * (Vector2) mTransform.up));
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
            float movementTimeInversed = 1 / MovementDuration;

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

        #endregion
    }
}