using UnityEngine;

namespace UnityTankBattalion
{
    public class PlayerLivesCounter : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Updates the player lives counter
        /// </summary>
        /// <param name="currentLives"></param>
        public void UpdatePlayerLifesCounter(int currentLives)
        {
            // If we have more lives than we have items, just enable them all
            if (currentLives > transform.childCount)
            {
                SetAllChildrenActive(true);
                return;
            }

            // Set all children inactive
            SetAllChildrenActive(false);
            
            // Now only enable the lives we have
            for (int i = 0; i < currentLives; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets all the children to the given state
        /// </summary>
        /// <param name="state"></param>
        private void SetAllChildrenActive(bool state)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}