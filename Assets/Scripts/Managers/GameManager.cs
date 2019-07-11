using UnityEngine;
using UnityEngine.Events;
using UnityTankBattalion.Events;
using UnityTankBattalion.Scoring;

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
        /// Fired when the players points are updated
        /// </summary>
        public IntEventListener.UnityIntEvent OnPlayerPointsUpdated;

        /// <summary>
        /// Called when the player needs to respawn as they have lives left
        /// </summary>
        public UnityEvent OnRespawnPlayer;

        /// <summary>
        /// Fired when the player has no lives left
        /// </summary>
        public UnityEvent OnGameOver;

        /// <summary>
        /// Fired when pause game has been requested
        /// </summary>
        public UnityEvent OnPauseGameRequested;

        /// <summary>
        /// Fired when we have a new high score to add
        /// </summary>
        public IntEventListener.UnityIntEvent OnNewHighScore;

        #endregion

        #region Private Variables

        /// <summary>
        /// The current number of lives the player has
        /// </summary>
        private int mCurrentLives;

        /// <summary>
        /// The current points the player has
        /// </summary>
        private int mCurrentPoints;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise the GameManager
            Init();
        }

        private void Start()
        {
            // Update our lives and points
            FirePlayerLivesUpdatedEvent();
            FirePlayerPointsUpdatedEvent();
        }

        private void Update()
        {
            // Check if we are being asked to pause the game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPauseGameRequested?.Invoke();
            }
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

            // Make sure we have valid lives
            mCurrentLives = Mathf.Min(mCurrentLives, StartingLives);

            // Fire the player lives updated event
            FirePlayerLivesUpdatedEvent();
        }

        /// <summary>
        /// Adds points to the players point count
        /// </summary>
        /// <param name="pointsToAdd"></param>
        public void AddPoints(int pointsToAdd)
        {
            // Add points
            mCurrentPoints += pointsToAdd;

            // Fire the player points updated event
            FirePlayerPointsUpdatedEvent();
        }

        /// <summary>
        /// Called when all levels are completed
        /// </summary>
        public void OnAllLevelsComplete()
        {
            // Check if we have a new high score
            CheckForNewHighScore();
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

            // Set the players starting points
            mCurrentPoints = 0;

            // Start the game paused
            Time.timeScale = 0f;
        }

        /// <summary>
        /// Called when the game is over
        /// </summary>
        private void GameOver()
        {
            Debug.Log("GameOver!");
            OnGameOver?.Invoke();

            // Update the high score if needed
            CheckForNewHighScore();
        }

        /// <summary>
        /// Fires the player lives updated event
        /// </summary>
        private void FirePlayerLivesUpdatedEvent()
        {
            // Fire an event that we have updated our lives count
            OnPlayerLivesUpdated?.Invoke(mCurrentLives);
        }

        /// <summary>
        /// Fires the player points updated event
        /// </summary>
        private void FirePlayerPointsUpdatedEvent()
        {
            // Fire an event that the players points have been updated
            OnPlayerPointsUpdated?.Invoke(mCurrentPoints);
        }

        /// <summary>
        /// Checks to see if we have a new high score 
        /// </summary>
        private void CheckForNewHighScore()
        {
            if (HighScoreManager.Instance.IsNewHighScore(mCurrentPoints))
            {
                OnNewHighScore?.Invoke(mCurrentPoints);
            }
        }

        #endregion
    }
}