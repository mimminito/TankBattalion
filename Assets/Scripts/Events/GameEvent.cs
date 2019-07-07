using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion.Events
{
    [CreateAssetMenu(menuName = "Events/New Game Event")]
    public class GameEvent : ScriptableObject
    {
        #region Public Variables

        /// <summary>
        /// Our listeners for this event
        /// </summary>
        public List<GameEventListener> Listeners;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires this event
        /// </summary>
        public void FireEvent()
        {
            // Loop through all listeners and tell them this event has been fired
            for (int i = 0; i < Listeners.Count; i++)
            {
                if (Listeners[i] != null)
                {
                    Listeners[i].OnEventFired();
                }
            }
        }

        /// <summary>
        /// Adds a new listener to our list
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(GameEventListener listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener from our list
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(GameEventListener listener)
        {
            Listeners.Remove(listener);
        }

        #endregion
    }
}