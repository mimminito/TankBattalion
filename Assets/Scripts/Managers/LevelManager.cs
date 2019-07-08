using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityTankBattalion.Events;

namespace UnityTankBattalion
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Public Variables

        /// <summary>
        /// The players Tank prefab
        /// </summary>
        [Header("Player")] public GameObject PlayerPrefab;

        /// <summary>
        /// The players spawn point
        /// </summary>
        public Transform PlayerSpawnPoint;

        /// <summary>
        /// The delay between finishing an old level and starting a new one
        /// </summary>
        [Header("Level Delays")] public float DelayBetweenLevels = 2f;

        /// <summary>
        /// Delay before the player is respawned
        /// </summary>
        [Header("Respawn")] public float RespawnDelay = 2f;

        /// <summary>
        /// The tilemaps which are used for collision
        /// </summary>
        [Header("Tilemaps")] public List<Tilemap> CollidableTilemaps;

        /// <summary>
        /// An event which is fired when the level is started
        /// </summary>
        [Header("Unity Events")] public UnityEvent OnLevelStarted;

        /// <summary>
        /// Called when our level counter is updated
        /// </summary>
        public IntEventListener.UnityIntEvent OnCurrentLevelCounterUpdated;

        #endregion

        #region Private Variables

        /// <summary>
        /// The current active players tank
        /// </summary>
        private GameObject mActivePlayer;

        /// <summary>
        /// Our current level
        /// </summary>
        private int mCurrentLevel = 1;

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Set the current level to 1
            mCurrentLevel = 1;

            // Initialise the player on start
            InstantiatePlayer();

            // Spawn the player
            SpawnPlayer();

            // Start the level
            StartLevel();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Spawns a player at the given spawn point
        /// </summary>
        public void SpawnPlayer()
        {
            if (mActivePlayer == null)
            {
                Debug.LogError("Cannot spawn player, no active player instantiated.");
                return;
            }

            // Spawn the player at the specified location
            mActivePlayer.transform.position = PlayerSpawnPoint.position;
            mActivePlayer.SetActive(true);
        }

        /// <summary>
        /// Respawns the player
        /// </summary>
        public void RespawnPlayer()
        {
            StartCoroutine(DoRespawnPlayer());
        }

        /// <summary>
        /// Called when the current level is completed
        /// </summary>
        public void OnLevelCompleted()
        {
            Debug.Log("OnLevelCompleted");
            
            StartCoroutine(LevelCompletedRoutine());
        }

        #endregion

        #region Private Variables

        /// <summary>
        /// Spanws the player at the spawn location
        /// </summary>
        private void InstantiatePlayer()
        {
            mActivePlayer = Pooling.GetFromPool(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
        }

        /// <summary>
        /// Starts the level and fires an event which can be listened to
        /// </summary>
        private void StartLevel()
        {
            // Ensure our level counter is displayed properly
            FireLevelCounterUpdatedEvent();

            // Fires the level start event
            OnLevelStarted?.Invoke();
        }

        /// <summary>
        /// Respawns the player
        /// </summary>
        /// <returns></returns>
        private IEnumerator DoRespawnPlayer()
        {
            // Wait
            yield return new WaitForSeconds(RespawnDelay);

            // Spawn the player
            SpawnPlayer();
        }

        /// <summary>
        /// Updated our current level counter
        /// </summary>
        private void FireLevelCounterUpdatedEvent()
        {
            OnCurrentLevelCounterUpdated?.Invoke(mCurrentLevel);
        }

        /// <summary>
        /// Routine called when the level has been completed
        /// </summary>
        /// <returns></returns>
        private IEnumerator LevelCompletedRoutine()
        {
            // Increase the level count
            mCurrentLevel++;
            FireLevelCounterUpdatedEvent();

            // Wait for our delay
            yield return new WaitForSeconds(DelayBetweenLevels);

            // Start the level
            StartLevel();
        }

        #endregion
    }
}