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

        /// <summary>
        /// Our cached transform
        /// </summary>
        private Transform mTransform;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void Start()
        {
            // Set our timer
            DetermineNextFrequency();

            // Start with some random input
            RandomiseInput();
        }

        private void Update()
        {
            // Check if we are stuck
            CheckIfStuck();

            // Updates the timer
            UpdateTimer();

            // Applies our current input
            ApplyInput();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise this component
        /// </summary>
        private void Init()
        {
            // Cache our transform
            mTransform = transform;

            // Grab our component
            mTankMovement = GetComponent<TankMovement>();
        }

        /// <summary>
        /// Checks to see if we are stuck moving in a direction we cannot
        /// </summary>
        private void CheckIfStuck()
        {
            // Check to see if we can move in our current direction
            if (mTankMovement.CheckCanMoveInDirection(mTransform.position, mTransform.up))
            {
                return;
            }

            // If we cannot, lets randomise our input again
            RandomiseInput();

            // Reset the timer
            DetermineNextFrequency();
        }

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
            // Get a random direction
            Vector2 newDirection = RandomiseDirection();

            // Check that we can move in that direction
            while (!mTankMovement.CheckCanMoveInDirection(mTransform.position, newDirection))
            {
                // Randomise our direction until we get a valid one
                newDirection = RandomiseDirection();
            }

            // Set our input to the new direction we have set
            mCurrentInput = newDirection;
        }

        /// <summary>
        /// Randomises our rotation
        /// </summary>
        private Vector2 RandomiseDirection()
        {
            Vector2 direction = Vector2.up;

            int index = Random.Range(0, 4);
            switch (index)
            {
                // Up
                case 0:
                    direction = Vector2.up;
                    break;
                // Down
                case 1:
                    direction = -Vector2.up;
                    break;
                // Left
                case 2:
                    direction = -Vector2.right;
                    break;
                // Right
                case 3:
                    direction = Vector2.right;
                    break;
            }

            return direction;
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