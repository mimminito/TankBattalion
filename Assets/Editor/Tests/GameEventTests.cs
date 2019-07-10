namespace UnityTankBattalion.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine.Events;
    using Events;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.TestTools;

    public class GameEventTests : MonoBehaviour
    {
        #region Public Methods

        [UnityTest]
        public IEnumerator TestGameEventFires()
        {
            // Create a GameEvent
            GameEvent gameEvent = ScriptableObject.CreateInstance<GameEvent>();
            gameEvent.Listeners = new List<GameEventListener>();

            // Create a GameObject
            GameObject listenerGO = new GameObject("ListenerGameObject");

            // Add GameEventListener to the GameObject
            GameEventListener gameEventListener = listenerGO.AddComponent<GameEventListener>();
            gameEventListener.runInEditMode = true;

            // Ensure we are added to the list
            gameEvent.AddListener(gameEventListener);

            // Add the GameEvent to the listener
            gameEventListener.TheEvent = gameEvent;

            // Did the event fire
            bool didFire = false;

            // Add a new listener
            gameEventListener.UnityEventHandler = new UnityEvent();
            gameEventListener.UnityEventHandler.AddListener(delegate { didFire = true; });

            // Fire the event
            gameEvent.FireEvent();

            yield return null;

            // Check if the event fired
            Assert.IsTrue(didFire);
        }

        #endregion
    }
}