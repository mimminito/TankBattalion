using UnityEngine;
using UnityEngine.UI;
using UnityTankBattalion.Scoring;

namespace UnityTankBattalion
{
    public class HighScoreSubmitter : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The players name input field
        /// </summary>
        public InputField PlayerNameInput;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached new high score
        /// </summary>
        private int mNewHighScore;

        #endregion

        #region Public Methods

        /// <summary>
        /// Cache the new high score 
        /// </summary>
        /// <param name="newHighScore"></param>
        public void CacheNewHighScore(int newHighScore)
        {
            mNewHighScore = newHighScore;
        }

        /// <summary>
        /// Submits a new high score
        /// </summary>
        public void SubmitNewHighScore()
        {
            // Check we have a valid name
            if (string.IsNullOrEmpty(PlayerNameInput.text))
            {
                return;
            }

            // Submit the high score
            HighScoreManager.Instance.AddHighScore(mNewHighScore, PlayerNameInput.text);
        }

        #endregion
    }
}