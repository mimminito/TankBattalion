using UnityEngine;
using UnityEngine.UI;
using UnityTankBattalion.Scoring;

namespace UnityTankBattalion
{
    public class HighScoreDisplayer : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Text component for the scores position
        /// </summary>
        public Text PositionText;

        /// <summary>
        /// Text component for the score
        /// </summary>
        public Text ScoreText;

        /// <summary>
        /// Text component for the players name
        /// </summary>
        public Text PlayerNameText;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise this row with its high score
        /// </summary>
        /// <param name="position"></param>
        /// <param name="highScore"></param>
        public void Init(int position, HighScore highScore)
        {
            // Update the position text
            PositionText.text = position.ToString();

            // Update the score text
            ScoreText.text = highScore.Score.ToString();

            // Update the players name
            PlayerNameText.text = highScore.PlayerName;
        }

        #endregion
    }
}