using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Public Variables

        [Header("Player")] public GameObject PlayerPrefab;
        public Transform PlayerSpawnPoint;

        [Header("Tilemaps")] public List<Tilemap> CollidableTilemaps;

        #endregion

        #region Private Variables

        private GameObject mActivePlayer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Initialise the player on start
            InstantiatePlayer();

            // Spawn the player
            SpawnPlayer();
        }

        #endregion

        #region Public Methods

        public void SpawnPlayer()
        {
            if (mActivePlayer == null)
            {
                Debug.LogError("Cannot spawn player, no active player instantiated.");
                return;
            }

            // Spawn the player at the specified location
            mActivePlayer.transform.position = PlayerSpawnPoint.position;
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

        #endregion
    }
}