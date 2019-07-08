using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityTankBattalion
{
    public class LoadSceneButton : MonoBehaviour
    {
        #region Public Variables

        public string SceneToLoad;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Button button = GetComponent<Button>();
            if (!button)
            {
                return;
            }

            button.onClick.AddListener(delegate { LoadScene(SceneToLoad); });
        }

        #endregion

        #region Private Methods

        private void LoadScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        #endregion
    }
}