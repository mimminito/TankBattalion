using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace UnityTankBattalion
{
    public class LaserBeamWeapon : BaseTankWeapon
    {
        #region Public Variables

        /// <summary>
        /// Amount of damage this projectile will deal to another entity
        /// </summary>
        [Header("Damage")] public int DamageToDeal = 10;

        /// <summary>
        /// The renderer used for the weapons beam
        /// </summary>
        [Header("Visuals")] public LineRenderer BeamRenderer;

        /// <summary>
        /// The duration for which the beam is enabled
        /// </summary>
        public float LaserEnabledDuration = 0.5f;

        /// <summary>
        /// The layer for the terrain
        /// </summary>
        [Header("Raycasting")] public LayerMask TerrainLayer;

        /// <summary>
        /// The layer for enemies
        /// </summary>
        public LayerMask EnemyLayer;

        /// <summary>
        /// The maximum length of the beam
        /// </summary>
        public float BeamDistance = 20f;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our cached transform
        /// </summary>
        private Transform mTransform;

        /// <summary>
        /// An array of hits, used for more efficient raycasting
        /// </summary>
        private readonly RaycastHit2D[] mHits = new RaycastHit2D[20];

        /// <summary>
        /// Timer for how long the beam is enabled
        /// </summary>
        private float mBeamTimer;

        /// <summary>
        /// Whether we are firing the beam
        /// </summary>
        private bool mIsFiring;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void Update()
        {
            // Check if we are firing
            if (mIsFiring)
            {
                // Update our timer
                mBeamTimer -= Time.deltaTime;

                // Check if our timer has finished
                if (mBeamTimer <= 0f)
                {
                    // Stop firing
                    StopFiringLaserBeam();
                    return;
                }

                // Fire our beam
                FireLaserBeam();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the weapon
        /// </summary>
        public override void FireWeapon()
        {
            //Set that we are firing
            mIsFiring = true;

            // Initialise our timer
            mBeamTimer = LaserEnabledDuration;

            // Init our beam renderer
            BeamRenderer.positionCount = 0;
            BeamRenderer.enabled = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise the weapon
        /// </summary>
        private void Init()
        {
            // Cache our transform
            mTransform = transform;

            // Reset the laser
            StopFiringLaserBeam();
        }

        /// <summary>
        /// Fires the laser beam
        /// </summary>
        private void FireLaserBeam()
        {
            Vector3 ourPosition = mTransform.position;

            // Raycast against the terrain to stop the beam
            int hits = Physics2D.RaycastNonAlloc(ourPosition, mTransform.up, mHits, BeamDistance, TerrainLayer);

            // Check if we have hit anything
            if (hits == 0)
            {
                return;
            }

            // Render the beam so it hits the wall
            BeamRenderer.positionCount = 2;
            BeamRenderer.SetPosition(0, ourPosition);
            BeamRenderer.SetPosition(1, mHits[0].point);

            // Raycast against all enemies
            float beamDistance = Vector2.Distance(ourPosition, mHits[0].point);
            hits = Physics2D.RaycastNonAlloc(ourPosition, mTransform.up, mHits, beamDistance, EnemyLayer);

            // Check if we have hit anything
            if (hits == 0)
            {
                return;
            }

            // Loop through all hits and then damage the enemies
            for (int i = 0; i < hits; i++)
            {
                // Grab the enemy health and kill them
                Health enemyHealth = mHits[i].transform.GetComponent<Health>();
                if (enemyHealth)
                {
                    enemyHealth.Damage(DamageToDeal);
                }
            }
        }

        /// <summary>
        /// Stops firing the laser beam
        /// </summary>
        private void StopFiringLaserBeam()
        {
            // Set our flag to stop firing
            mIsFiring = false;

            // Clear the line renderer
            BeamRenderer.positionCount = 0;

            // Disable the beam renderer
            BeamRenderer.enabled = false;
        }

        #endregion
    }
}