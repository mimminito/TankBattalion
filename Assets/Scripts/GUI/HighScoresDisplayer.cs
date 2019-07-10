using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTankBattalion.Scoring;

namespace UnityTankBattalion
{
    public class HighScoresDisplayer : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Container under which all high scores will be displayed
        /// </summary>
        [Header("Layouts")] public RectTransform HighScoresLayoutContainer;

        /// <summary>
        /// ScrollView for the high scores
        /// </summary>
        public GameObject HighScoresScrollView;

        /// <summary>
        /// Layout shown when there are no high scores
        /// </summary>
        public GameObject NoHighScoresLayout;


        /// <summary>
        /// Prefab for the current high score
        /// </summary>
        [Header("Row Prefabs")] public GameObject HighScorePrefab;

        /// <summary>
        /// Prefab for all other scores
        /// </summary>
        public GameObject ScorePrefab;

        /// <summary>
        /// Scene to load on back pressed
        /// </summary>
        [Header("Back Control")] public string SceneToLoadOnBack = "MainMenu";

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Hide the no high scores layout
            NoHighScoresLayout.SetActive(false);

            // Display the high scores
            DisplayHighScores();
        }

        private void Update()
        {
            // Check to see if we are trying to go back to the Main Menu
            CheckForBack();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the back button actions via keyboard input
        /// </summary>
        private void CheckForBack()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        /// <summary>
        /// Displays the high scores in a list
        /// </summary>
        private void DisplayHighScores()
        {
            // Clear the display of scores
            ClearDisplay();

            // Get the high scores
            var highScores = HighScoreManager.Instance.HighScores;

            // If we have no high scores inform the player
            if (highScores == null || highScores.Count <= 0)
            {
                NoHighScoresLayout.SetActive(true);
                HighScoresScrollView.gameObject.SetActive(false);
                return;
            }

            // Sort high scores
            highScores.Sort((score1, score2) => score2.Score.CompareTo(score1.Score));

            // Loop through all high scores
            for (int i = 0; i < highScores.Count; i++)
            {
                // Spawn the row
                GameObject row = Pooling.GetFromPool(i == 0 ? HighScorePrefab : ScorePrefab, Vector3.zero, Quaternion.identity);

                // Reset its transform values
                row.transform.SetParent(HighScoresLayoutContainer);
                row.transform.localScale = Vector3.one;

                // Update the rows display
                HighScoreDisplayer highScoreDisplayer = row.GetComponent<HighScoreDisplayer>();
                if (highScoreDisplayer != null)
                {
                    highScoreDisplayer.Init(i + 1, highScores[i]);
                }
            }
        }

        /// <summary>
        /// Clears the currently displayed scores
        /// </summary>
        private void ClearDisplay()
        {
            // Clear the layout first
            for (int i = 0; i < HighScoresLayoutContainer.childCount; i++)
            {
                Destroy(HighScoresLayoutContainer.GetChild(i).gameObject);
            }
        }

        #endregion
    }
}