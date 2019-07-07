using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace UnityTankBattalion.Scoring
{
    public class HighScoreManager : Singleton<HighScoreManager>
    {
        #region Private Classes

        [Serializable]
        private class HighScoresContainer
        {
            /// <summary>
            /// The list of high scores
            /// </summary>
            [SerializeField] public List<HighScore> HighScores = new List<HighScore>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The list of high scores
        /// </summary>
        public List<HighScore> HighScores = new List<HighScore>();

        [Header("Unity Events")] public UnityEvent OnHighScoreAdded;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current high score
        /// </summary>
        public int CurrentHighScore
        {
            get
            {
                int currentHighScore = 0;

                for (int i = 0; i < HighScores.Count; i++)
                {
                    if (HighScores[i].Score > currentHighScore)
                    {
                        currentHighScore = HighScores[i].Score;
                    }
                }

                return currentHighScore;
            }
        }

        #endregion

        #region Private Variables

        /// <summary>
        /// Key for the high scores player prefs
        /// </summary>
        private const string mPlayerPrefsHighScoresKey = "UTB_HighScores";

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            // Retrieve high scores from player prefs
            DeserializeHighScores();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a high score to our list
        /// </summary>
        /// <param name="score"></param>
        /// <param name="playerName"></param>
        public void AddHighScore(int score, string playerName = "Player 1")
        {
            // Check to see if this is a new high score
            if (!IsNewHighScore(score))
            {
                return;
            }

            // Add the high score to the list
            HighScores.Add(new HighScore()
            {
                Score = score,
                PlayerName = playerName
            });

            // Fire an event that a new high score has been added
            OnHighScoreAdded?.Invoke();

            // Persist the high scores
            SerializeHighScores();
        }

        /// <summary>
        /// Checks to see if the score is a new high score
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool IsNewHighScore(int score)
        {
            bool result = true;
            foreach (HighScore highScore in HighScores)
            {
                if (highScore.Score <= score)
                {
                    continue;
                }

                result = false;
                break;
            }

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Persists the high scores into the player prefs
        /// </summary>
        private void SerializeHighScores()
        {
            HighScoresContainer container = new HighScoresContainer
            {
                HighScores = HighScores
            };

            string json = JsonUtility.ToJson(container);
            PlayerPrefs.SetString(mPlayerPrefsHighScoresKey, json);
        }

        /// <summary>
        /// Retrieves the high scores from player prefs
        /// </summary>
        private void DeserializeHighScores()
        {
            string json = PlayerPrefs.GetString(mPlayerPrefsHighScoresKey, "");
            if (string.IsNullOrEmpty(json))
            {
                HighScores = new List<HighScore>();
                return;
            }

            HighScoresContainer container = JsonUtility.FromJson<HighScoresContainer>(json);
            HighScores = container.HighScores;
        }

        #endregion
    }
}