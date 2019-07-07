using UnityEngine;
using UnityEngine.AI;

namespace UnityTankBattalion
{
    public class PlayerTankController : MonoBehaviour
    {
        #region Private Variables

        // Components
        private TankMovement mTankMovement;
        private TankWeapon mTankWeapon;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Garb our components
            mTankMovement = GetComponent<TankMovement>();
            mTankWeapon = GetComponent<TankWeapon>();
        }

        private void Update()
        {
            // Handles the movement input
            HandleMovementInput();

            // Handle the shooting input
            HandleShootingInput();
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
            if (Input.GetKeyUp(KeyCode.Space))
            {
                mTankWeapon.FireWeapon();
            }
        }

        #endregion
    }
}