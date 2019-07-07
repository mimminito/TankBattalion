using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion.Events
{
    [CreateAssetMenu(menuName = "Events/New GameObject Event")]
    public class GameObjectEvent : GameEvent
    {
        #region Public Variables

        /// <summary>
        /// Our listeners for this event
        /// </summary>
        public List<GameObjectEventListener> GameObjectEventListeners;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires the event, passing through a GameObject
        /// </summary>
        /// <param name="go"></param>
        public void FireEvent(GameObject go)
        {
            // Loop through all listeners and tell them this event has been fired
            for (int i = 0; i < GameObjectEventListeners.Count; i++)
            {
                if (GameObjectEventListeners[i] != null)
                {
                    GameObjectEventListeners[i].OnEventFired(go);
                }
            }
        }

        /// <summary>
        /// Adds a new listener to our list
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(GameObjectEventListener listener)
        {
            GameObjectEventListeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener from our list
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(GameObjectEventListener listener)
        {
            GameObjectEventListeners.Remove(listener);
        }

        #endregion
    }
}