using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityTankBattalion
{
    public class SimpleEnemyTankInputController : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The minimum amount of time that can pass before we randomise input
        /// </summary>
        public float MinFrequency = 3f;
        
        /// <summary>
        /// The maximum amount of time that can pass before we randomise input
        /// </summary>
        public float MaxFrequency = 5f;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our tank movement component
        /// </summary>
        private TankMovement mTankMovement;

        /// <summary>
        /// Timer for deciding our next command
        /// </summary>
        private float mTimer;

        /// <summary>
        /// Our current input 
        /// </summary>
        private Vector2 mCurrentInput;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Grab our component
            mTankMovement = GetComponent<TankMovement>();

            // Set our timer
            DetermineNextFrequency();
            
            // Start with some random input
            RandomiseInput();
        }

        private void Update()
        {
            // Updates the timer
            UpdateTimer();
            
            // Applies our current input
            ApplyInput();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the timer until we can randomise a new movement
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
                RandomiseInput();
            }
        }

        /// <summary>
        /// Randomise our movement
        /// </summary>
        private void RandomiseInput()
        {
            int moveHorizontal = Random.Range(0, 2);

            // If we are moving horizontal
            if (moveHorizontal == 0)
            {
                int horizontalDirection = Random.Range(0, 2);
                mCurrentInput = new Vector2(horizontalDirection == 0 ? -1f : 1f, 0f);
            }
            else
            {
                int verticalMovement = Random.Range(0, 2);
                mCurrentInput = new Vector2(0f, verticalMovement == 0 ? -1f : 1f);
            }
        }

        /// <summary>
        /// Applies the input to the TankMovement script
        /// </summary>
        private void ApplyInput()
        {
            mTankMovement.SetInput(mCurrentInput);
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