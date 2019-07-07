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
        public List<GameObjectEventListener> GameObjectListeners;

        #endregion

        #region Public Methods

        public void FireEvent(GameObject go)
        {
            // Loop through all listeners and tell them this event has been fired
            for (int i = 0; i < GameObjectListeners.Count; i++)
            {
                if (GameObjectListeners[i] != null)
                {
                    GameObjectListeners[i].OnEventFired(go);
                }
            }
        }

        /// <summary>
        /// Adds a new listener to our list
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(GameObjectEventListener listener)
        {
            GameObjectListeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener from our list
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(GameObjectEventListener listener)
        {
            GameObjectListeners.Remove(listener);
        }

        #endregion
    }
}