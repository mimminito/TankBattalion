using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityTankBattalion
{
    public class LoadSceneButton : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The scene to load on button press
        /// </summary>
        public string SceneToLoad;

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Check if we have a button
            Button button = GetComponent<Button>();
            if (!button)
            {
                return;
            }

            // Add a listener for when we have clicked it
            button.onClick.AddListener(delegate { LoadScene(SceneToLoad); });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a given scene
        /// </summary>
        /// <param name="sceneToLoad"></param>
        private void LoadScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        #endregion
    }
}