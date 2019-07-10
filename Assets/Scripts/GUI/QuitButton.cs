using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTankBattalion
{
    public class QuitButton : MonoBehaviour
    {
        #region Unity Methods

        private void Start()
        {
            // Check we have a button
            Button button = GetComponent<Button>();
            if (!button)
            {
                return;
            }

            // Add a listener for when we have clicked the button
            button.onClick.AddListener(QuitApp);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Quit the app
        /// </summary>
        private void QuitApp()
        {
            // If we are in the editor, and playing, stop playing
            if (Application.isEditor && Application.isPlaying)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                // Quit the app
                Application.Quit();
            }
        }

        #endregion
    }
}