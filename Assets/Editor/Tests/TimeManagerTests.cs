using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnityTankBattalion.Tests
{
    public class TimeManagerTests : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Test we can pause the game through Time.timeScale changes
        /// </summary>
        [UnityTest]
        public IEnumerator TestCanPauseGame()
        {
            // Create a TimeManager
            GameObject timeManagerGO = new GameObject("TimeManager");
            TimeManager timeManager = timeManagerGO.AddComponent<TimeManager>();

            // Get the current time scale
            float currentTimeScale = Time.timeScale;

            // Pause the game
            timeManager.PauseGame();

            // Check we have changed the time scale
            Assert.AreNotEqual(currentTimeScale, Time.timeScale);
            Assert.AreEqual(Time.timeScale, 0f);

            yield return null;
        }

        /// <summary>
        /// Test we can resume the game through Time.timeScale changes
        /// </summary>
        [UnityTest]
        public IEnumerator TestCanResumeGame()
        {
            // Create a TimeManager
            GameObject timeManagerGO = new GameObject("TimeManager");
            TimeManager timeManager = timeManagerGO.AddComponent<TimeManager>();
            timeManager.runInEditMode = true;

            // Update time scale
            Time.timeScale = 0f;

            // Check we are paused
            Assert.AreEqual(0, Time.timeScale);

            // Resume the game
            timeManager.ResumeGame();

            // Check we have changed the time scale
            Assert.AreNotEqual(0, Time.timeScale);
            Assert.AreEqual(Time.timeScale, 1f);
            
            yield return null;
        }

        #endregion
    }
}