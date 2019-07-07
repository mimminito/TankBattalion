using UnityEngine;
using UnityEngine.Events;

namespace UnityTankBattalion.Events
{
    public class IntEventListener : MonoBehaviour
    {
        #region Classes

        [System.Serializable]
        public class UnityIntEvent : UnityEvent<int>
        {
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// The event we should be listening for
        /// </summary>
        [Header("Event")] public IntEvent TheEvent;

        /// <summary>
        /// A unity event this is fired when our event has been fired
        /// </summary>
        [Header("Unity Event")] public UnityIntEvent UnityEventHandler;

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
        public void OnEventFired(int val)
        {
            UnityEventHandler.Invoke(val);
        }

        #endregion
    }
}