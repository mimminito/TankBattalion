using UnityEngine;

namespace UnityTankBattalion
{
    public class TimeManager : MonoBehaviour
    {

        #region Public Methods

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }

        #endregion
        
    }
}