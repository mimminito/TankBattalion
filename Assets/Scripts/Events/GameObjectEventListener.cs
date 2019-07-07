using UnityEngine;
using UnityEngine.Events;

namespace UnityTankBattalion.Events
{
    public class GameObjectEventListener : MonoBehaviour
    {
        #region Classes

        [System.Serializable]
        public class UnityGameObjectEvent : UnityEvent<GameObject>
        {
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// The event we should be listening for
        /// </summary>
        [Header("Event")] public GameObjectEvent TheEvent;

        /// <summary>
        /// A unity event this is fired when our event has been fired
        /// </summary>
        [Header("Unity Event")] public UnityGameObjectEvent UnityEventHandler;

        #endregion
        
        #region Unity Methods

        private void OnEnable()
        {
            if (TheEvent == null)
            {
                return;
            }

            // Add a listener
            TheEvent.AddListener(this);
        }

        private void OnDisable()
        {
            if (TheEvent == null)
            {
                return;
            }

            // Remove the listener
            TheEvent.RemoveListener(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when this event was fired
        /// </summary>
        public void OnEventFired(GameObject go)
        {
            UnityEventHandler.Invoke(go);
        }

        #endregion
    }
}