namespace UnityTankBattalion.Tests
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.TestTools;
    using Scoring;

    public class HighScoreManagerTests : MonoBehaviour
    {
        #region Public Methods

        [UnityTest]
        public IEnumerator TestCanAddHighScore()
        {
            // Create a HighScoreManager
            GameObject highScoreManagerGO = new GameObject("HighScoreManager");
            HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
            highScoreManager.runInEditMode = true;

            yield return null;

            // Ensure we have no high scores
            highScoreManager.HighScores.Clear();

            // Add a high score
            highScoreManager.AddHighScore(1000, "Test Player", false);

            // Check we have one high score
            Assert.AreEqual(highScoreManager.HighScores.Count, 1);
        }

        /// <summary>
        /// Tests to ensure we can add a new high score to the HighScoreManager when one already exists
        /// </summary>
        [UnityTest]
        public IEnumerator TestCanAddNewHighScore()
        {
            // Create a HighScoreManager
            GameObject highScoreManagerGO = new GameObject("HighScoreManager");
            HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
            highScoreManager.runInEditMode = true;

            yield return null;

            // Ensure we have no high scores
            highScoreManager.HighScores.Clear();

            // Add a high score
            highScoreManager.AddHighScore(1000, "Test Player 1", false);

            // Check we have one high score
            Assert.AreEqual(highScoreManager.HighScores.Count, 1);

            // Add a new high score
            highScoreManager.AddHighScore(1001, "Test Player 2", false);

            // Check we have two high scores
            Assert.AreEqual(highScoreManager.HighScores.Count, 2);
        }

        /// <summary>
        /// Tests to check we can get the current high score
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestCanGetCurrentHighScore()
        {
            // Create a HighScoreManager
            GameObject highScoreManagerGO = new GameObject("HighScoreManager");
            HighScoreManager highScoreManager = highScoreManagerGO.AddComponent<HighScoreManager>();
            highScoreManager.runInEditMode = true;

            yield return null;

            // Ensure we have no high scores
            highScoreManager.HighScores.Clear();

            // Add 3 high scores
            highScoreManager.AddHighScore(1000, "Test Player 1", false); // Add a new high score
            highScoreManager.AddHighScore(1001, "Test Player 2", false); // Add a new high score
            highScoreManager.AddHighScore(1002, "Test Player 3", false);

            // Check we have 3 high scores
            Assert.AreEqual(highScoreManager.HighScores.Count, 3);

            // Get the current high score
            float currentHighScore = highScoreManager.CurrentHighScore;

            // Make sure the current high score is correct
            Assert.AreEqual(1002, currentHighScore);
        }

        #endregion
    }
}