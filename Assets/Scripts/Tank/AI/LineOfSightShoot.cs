using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace UnityTankBattalion
{
    public class LineOfSightShoot : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Our fire rate
        /// </summary>
        [Header("Shooting Config")] public float FireRate = 1f;

        /// <summary>
        /// The layer to check against when raycasting
        /// </summary>
        public LayerMask ShootingLayer;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our weapon handler
        /// </summary>
        private TankWeaponHandler mWeaponHandler;

        /// <summary>
        /// Our hits array
        /// </summary>
        private readonly RaycastHit2D[] mHits = new RaycastHit2D[1];

        /// <summary>
        /// Our cooldown timer
        /// </summary>
        private float mCooldownTimer;

        /// <summary>
        /// Whether we are cooling down
        /// </summary>
        private bool mIsCoolingDown;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void Update()
        {
            // If we are not firing, check if we can shoot
            if (!mIsCoolingDown)
            {
                CheckCanShoot();
            }

            if (mIsCoolingDown)
            {
                // Update our timer
                mCooldownTimer -= Time.deltaTime;

                if (mCooldownTimer <= 0f)
                {
                    // Set we are not firing
                    mIsCoolingDown = false;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise
        /// </summary>
        private void Init()
        {
            // Grab our weapon handler
            mWeaponHandler = GetComponent<TankWeaponHandler>();

            // We are not cooling down
            mIsCoolingDown = false;
        }

        /// <summary>
        /// Check if we can shoot
        /// </summary>
        private void CheckCanShoot()
        {
            // Raycast to find any hits
            int hits = Physics2D.RaycastNonAlloc(mWeaponHandler.BarrelEndTransform.position, transform.up, mHits, Mathf.Infinity, ShootingLayer);

            // Check if we have a hit
            if (hits == 0)
            {
                return;
            }

            // Set we are cooling down
            mIsCoolingDown = true;
            mCooldownTimer = FireRate;
            
            // Fire our weapon
            mWeaponHandler.FireWeapon();
        }

        #endregion
    }
}