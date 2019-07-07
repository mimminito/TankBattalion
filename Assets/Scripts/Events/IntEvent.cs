using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion.Events
{
    [CreateAssetMenu(menuName = "Events/New Int Event")]
    public class IntEvent : GameEvent
    {
        #region Public Variables

        /// <summary>
        /// Our listeners for this event
        /// </summary>
        public List<IntEventListener> IntEventListeners;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the event, passing through an Integer
        /// </summary>
        /// <param name="val"></param>
        public void FireEvent(int val)
        {
            // Loop through all listeners and tell them this event has been fired
            for (int i = 0; i < IntEventListeners.Count; i++)
            {
                if (IntEventListeners[i] != null)
                {
                    IntEventListeners[i].OnEventFired(val);
                }
            }
        }

        /// <summary>
        /// Adds a new listener to our list
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(IntEventListener listener)
        {
            IntEventListeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener from our list
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(IntEventListener listener)
        {
            IntEventListeners.Remove(listener);
        }

        #endregion
    }
}