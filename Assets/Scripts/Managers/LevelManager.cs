using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

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

        #endregion

        #region Private Variables

        /// <summary>
        /// The current active players tank
        /// </summary>
        private GameObject mActivePlayer;

        #endregion

        #region Unity Methods

        private void Start()
        {
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

        #endregion
    }
}