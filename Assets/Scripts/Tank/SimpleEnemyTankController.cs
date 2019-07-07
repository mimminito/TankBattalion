using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityTankBattalion
{
    public class SimpleEnemyTankController : MonoBehaviour
    {
        #region Private Variables

        /// <summary>
        /// Our tank movement component
        /// </summary>
        private TankMovement mTankMovement;

        /// <summary>
        /// Timer for deciding our next command
        /// </summary>
        private float mCommandTimer;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Grab our component
            mTankMovement = GetComponent<TankMovement>();

            // Set our timer
            mCommandTimer = mTankMovement.MovementDuration;
        }

        private void Update()
        {
            // Update our timer
            mCommandTimer -= Time.deltaTime;

            // Check if our timer has elapsed
            if (mCommandTimer <= 0f)
            {
                // Reset our timer
                mCommandTimer = mTankMovement.MovementDuration;

                // Decide on some movement
                RandomiseMovement();
            }
        }

        /// <summary>
        /// Randomise our movement
        /// </summary>
        private void RandomiseMovement()
        {
            int moveHorizontal = Random.Range(0, 2);

            // If we are moving horizontal
            if (moveHorizontal == 0)
            {
                int horizontalDirection = Random.Range(0, 2);
                mTankMovement.SetInput(new Vector2(horizontalDirection == 0 ? -1f : 1f, 0f));
            }
            else
            {
                int verticalMovement = Random.Range(0, 2);
                mTankMovement.SetInput(new Vector2(0f, verticalMovement == 0 ? -1f : 1f));
            }
        }

        #endregion
    }
}