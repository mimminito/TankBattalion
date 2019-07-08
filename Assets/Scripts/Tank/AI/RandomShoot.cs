using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

namespace UnityTankBattalion
{
    [RequireComponent(typeof(TankWeapon))]
    public class RandomShoot : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The minimum amount of time that can pass before we randomise shooting
        /// </summary>
        public float MinFrequency = 3f;

        /// <summary>
        /// The maximum amount of time that can pass before we randomise shooting
        /// </summary>
        public float MaxFrequency = 5f;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our timer
        /// </summary>
        private float mTimer;

        /// <summary>
        /// Our weapon
        /// </summary>
        private TankWeapon mTankWeapon;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise this component
            Init();
        }

        private void Update()
        {
            // Update our timer
            UpdateTimer();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise this component
        /// </summary>
        private void Init()
        {
            // Grab our weapon
            mTankWeapon = GetComponent<TankWeapon>();

            // If we do not have a weapon attached then return out
            if (!mTankWeapon)
            {
                return;
            }

            DetermineNextFrequency();
        }

        /// <summary>
        /// Updates our timer
        /// </summary>
        private void UpdateTimer()
        {
            // Update our timer
            mTimer -= Time.deltaTime;

            // Check if our timer has elapsed
            if (mTimer <= 0f)
            {
                // Randomise our timer
                DetermineNextFrequency();

                // Decide on some movement
                FireWeapon();
            }
        }

        /// <summary>
        /// Fires our weapon
        /// </summary>
        /// <returns></returns>
        private void FireWeapon()
        {
            // Check we have a weapon
            if (!mTankWeapon)
            {
                return;
            }

            // Fire the weapon
            mTankWeapon.FireWeapon();
        }

        /// <summary>
        /// Determines our next frequency value for the timer
        /// </summary>
        /// <returns></returns>
        private void DetermineNextFrequency()
        {
            mTimer = Random.Range(MinFrequency, MaxFrequency);
        }

        #endregion
    }
}