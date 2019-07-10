using UnityEngine;
using UnityEngine.UI;
using UnityTankBattalion.Scoring;

namespace UnityTankBattalion
{
    public class HighScoreCounter : MonoBehaviour
    {
        #region Private Variables

        /// <summary>
        /// Our text component
        /// </summary>
        private Text mText;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Grab our text component
            mText = GetComponent<Text>();
        }

        private void Start()
        {
            // Update our current high score on start
            mText.text = HighScoreManager.Instance.CurrentHighScore.ToString();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the high score, if we can
        /// </summary>
        /// <param name="newScore"></param>
        public void UpdateHighScore(int newScore)
        {
            // Check to see if we have a new high score
            if (HighScoreManager.Instance.IsNewHighScore(newScore))
            {
                // Update the counter
                mText.text = newScore.ToString();
            }
        }

        #endregion
    }
}