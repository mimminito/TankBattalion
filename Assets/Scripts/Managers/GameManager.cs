using UnityEngine;
using UnityEngine.Events;
using UnityTankBattalion.Events;

namespace UnityTankBattalion
{
    public class GameManager : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Number of lives the player can start with
        /// </summary>
        [Header("Player Lives")] public int StartingLives = 3;


        /// <summary>
        /// Fired when the players lives have been updated
        /// </summary>
        [Header("Unity Events")] public IntEventListener.UnityIntEvent OnPlayerLivesUpdated;

        /// <summary>
        /// Called when the player needs to respawn as they have lives left
        /// </summary>
        public UnityEvent OnRespawnPlayer;

        /// <summary>
        /// Fired when the player has no lives left
        /// </summary>
        public UnityEvent OnGameOver;

        #endregion

        #region Private Variables

        /// <summary>
        /// The current number of lives the player has
        /// </summary>
        private int mCurrentLives;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise the GameManager
            Init();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Subtracts a life from the players current lives
        /// </summary>
        public void LoseLife()
        {
            // Subtract a life
            mCurrentLives--;

            // Fire the player lives updated event
            FirePlayerLivesUpdatedEvent();

            // Check if we have no lives left
            if (mCurrentLives <= 0)
            {
                // We have no lives left
                GameOver();
            }
            else
            {
                // Fire the respawn player event
                OnRespawnPlayer?.Invoke();
            }
        }

        /// <summary>
        /// Gives a single life to the player
        /// </summary>
        public void GiveLife()
        {
            // Add a single life
            mCurrentLives++;

            // Fire the player lives updated event
            FirePlayerLivesUpdatedEvent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise the GameManager
        /// </summary>
        private void Init()
        {
            // Set the players current number of lives            
            mCurrentLives = StartingLives;
        }

        /// <summary>
        /// Called when the game is over
        /// </summary>
        private void GameOver()
        {
            Debug.Log("GameOver!");
            OnGameOver?.Invoke();
        }

        /// <summary>
        /// Fires the player lives updated event
        /// </summary>
        private void FirePlayerLivesUpdatedEvent()
        {
            // Fire an event that we have lost a life
            OnPlayerLivesUpdated?.Invoke(mCurrentLives);
        }

        #endregion
    }
}