using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    public class PlayerWeaponSwapper : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// List of weapons we can use
        /// </summary>
        public List<GameObject> PlayerWeapons;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our weapon handler
        /// </summary>
        private TankWeaponHandler mTankWeaponHandler;

        /// <summary>
        /// Our current weapon index
        /// </summary>
        private int mCurrentWeaponIndex;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Get our weapon handler
            mTankWeaponHandler = GetComponent<TankWeaponHandler>();
        }

        private void Update()
        {
            // Swap weapons
            HandleWeaponSwapping();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the swapping of weapons
        /// </summary>
        private void HandleWeaponSwapping()
        {
            // Save our last index
            int lastSelectedWeaponIndex = mCurrentWeaponIndex;

            // Check if we have selected the previous weapon in our list
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                if (mCurrentWeaponIndex <= 0)
                {
                    mCurrentWeaponIndex = PlayerWeapons.Count - 1;
                }
                else
                {
                    mCurrentWeaponIndex--;
                }
            }

            // Get if we have selected the next weapon in our list
            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                if (mCurrentWeaponIndex >= PlayerWeapons.Count - 1)
                {
                    mCurrentWeaponIndex = 0;
                }
                else
                {
                    mCurrentWeaponIndex++;
                }
            }

            // If we have changed, swap weapon
            if (lastSelectedWeaponIndex != mCurrentWeaponIndex)
            {
                mTankWeaponHandler.SwapWeapon(PlayerWeapons[mCurrentWeaponIndex]);
            }
        }

        #endregion
    }
}