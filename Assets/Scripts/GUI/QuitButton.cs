using System.Collections;
using System.Collections.Generic;
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
            Button button = GetComponent<Button>();
            if (!button)
            {
                return;
            }

            button.onClick.AddListener(QuitApp);
        }

        #endregion

        #region Private Methods

        private void QuitApp()
        {
            if (Application.isEditor && Application.isPlaying)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }

        #endregion
    }
}