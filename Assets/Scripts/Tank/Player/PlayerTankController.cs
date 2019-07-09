using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace UnityTankBattalion
{
    public class PlayerTankController : MonoBehaviour
    {
        #region Private Variables

        // Components
        private TankMovement mTankMovement;
        private TankWeaponHandler mTankWeaponHandlerHandler;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Garb our components
            mTankMovement = GetComponent<TankMovement>();
            mTankWeaponHandlerHandler = GetComponent<TankWeaponHandler>();
        }

        private void Update()
        {
            // Handles the movement input
            HandleMovementInput();

            // Handle the shooting input
            HandleShootingInput();

            // DEBUG
            if (Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<Health>().Kill();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks for and handles the input for movement
        /// </summary>
        private void HandleMovementInput()
        {
            Vector2 inputVector = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                inputVector.y = 1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                inputVector.y = -1f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                inputVector.x = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                inputVector.x = 1f;
            }
            else
            {
                inputVector = Vector2.zero;
            }

            // Apply our input
            mTankMovement.SetInput(inputVector);
        }

        /// <summary>
        /// Checks for and handles the input for shooting
        /// </summary>
        private void HandleShootingInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mTankWeaponHandlerHandler.FireWeapon();
            }
        }

        #endregion
    }
}