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
        /// Level data
        /// </summary>
        [Header("Level Data")] public LevelsDataHolder LevelsData;

        /// <summary>
        /// The parent for the tilemap to be spawned under
        /// </summary>
        public Transform TilemapParent;

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
        /// An event which is fired when the level is started
        /// </summary>
        [Header("Unity Events")] public UnityEvent OnLevelStarted;

        /// <summary>
        /// Called when our level counter is updated
        /// </summary>
        public IntEventListener.UnityIntEvent OnCurrentLevelCounterUpdated;

        /// <summary>
        /// An event called when all levels have been completed
        /// </summary>
        public UnityEvent OnAllLevelsComplete;

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

        /// <summary>
        /// Our current tilemap
        /// </summary>
        private GameObject mCurrentTilemapGO;

        #endregion

        #region Properties

        /// <summary>
        /// The current level info
        /// </summary>
        public LevelsDataHolder.LevelInfo CurrentLevelInfo
        {
            get { return LevelsData.Levels[mCurrentLevel - 1]; }
        }

        /// <summary>
        /// The tilemaps which are used for collision
        /// </summary>
        public List<Tilemap> CollidableTilemaps { get; private set; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Initialise our list
            CollidableTilemaps = new List<Tilemap>();

            // Set the current level to 1
            mCurrentLevel = 1;

            // Initialise the player on start
            InstantiatePlayer();

            // Spawn the player
            SpawnPlayer();

            // Setup the level
            SetupLevel();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            // Start the level
            StartLevel();
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
        /// Spawns a player at the given spawn point
        /// </summary>
        private void SpawnPlayer()
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
        /// Initialises our level with its tilemap
        /// </summary>
        private void SetupLevel()
        {
            // Get the current level info
            LevelsDataHolder.LevelInfo currentLevelInfo = LevelsData.Levels[mCurrentLevel - 1];

            // Destroy our current tilemap if we have one
            if (mCurrentTilemapGO)
            {
                Destroy(mCurrentTilemapGO);
            }

            // Spawn the new tilemap
            mCurrentTilemapGO = Pooling.GetFromPool(currentLevelInfo.LevelTilemap.gameObject, Vector3.zero, Quaternion.identity);
            mCurrentTilemapGO.transform.SetParent(TilemapParent);
            mCurrentTilemapGO.transform.position = Vector3.zero;
            mCurrentTilemapGO.transform.localScale = Vector3.one;

            // Setup our collidable tilemaps
            CollidableTilemaps.Clear();
            CollidableTilemaps.Add(mCurrentTilemapGO.GetComponentInChildren<Tilemap>());
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
            InstantiatePlayer();
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
            // First check if we have completed all the levels
            if (mCurrentLevel == LevelsData.Levels.Count)
            {
                // Fire an event that all levels are complete
                OnAllLevelsComplete?.Invoke();
                yield break;
            }

            // Wait for our delay
            yield return new WaitForSeconds(DelayBetweenLevels);

            // Increase the level count
            mCurrentLevel++;
            FireLevelCounterUpdatedEvent();

            // Respawn the player
            SpawnPlayer();

            // Setup the level
            SetupLevel();

            // Start the level
            StartLevel();
        }

        #endregion
    }
}