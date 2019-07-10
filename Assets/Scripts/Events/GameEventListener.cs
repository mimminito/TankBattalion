using UnityEngine;
using UnityEngine.Events;

namespace UnityTankBattalion.Events
{
    public class GameEventListener : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The GameEvent we should be listening for
        /// </summary>
        [Header("Event")] public GameEvent TheEvent;

        /// <summary>
        /// A unity event this is fired when our event has been fired
        /// </summary>
        [Header("Unity Event")] public UnityEvent UnityEventHandler;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            // Check we have an event
            if (TheEvent == null)
            {
                return;
            }

            // Add a listener
            TheEvent.AddListener(this);
        }

        private void OnDisable()
        {
            // Check we have an event
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
        public void OnEventFired()
        {
            UnityEventHandler.Invoke();
        }

        #endregion
    }
}