using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityTankBattalion.Events;

namespace UnityTankBattalion
{
    public class EnemyManager : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The enemy tank prefab
        /// </summary>
        [Header("Enemy Tank")] public GameObject EnemyTankPrefab;

        /// <summary>
        /// Enemy tank spawn locations
        /// </summary>
        [Header("Spawning")] public List<Transform> SpawnPoints;

        /// <summary>
        /// Initial delay before spawning starts
        /// </summary>
        public float InitialSpawnDelay = 2f;

        /// <summary>
        /// A delay between spawning tanks
        /// </summary>
        public float DelayBetweenSpawns = 5f;

        /// <summary>
        /// Maximum enemy tanks that can be spawned
        /// </summary>
        public int MaxEnemyTanks = 10;

        /// <summary>
        /// Fired when the enemy tank count has updated
        /// </summary>
        [Header("Unity Events")] public IntEventListener.UnityIntEvent OnEnemyTankCountUpdated;

        /// <summary>
        /// Fired when all enemy tanks have been killed
        /// </summary>
        public UnityEvent OnAllEnemyTanksKilled;

        #endregion

        #region Private Variables

        /// <summary>
        /// Routine used to spawn enemy tanks 
        /// </summary>
        private IEnumerator mSpawnEnemiesRoutine;

        /// <summary>
        /// Whether enemy tanks can spawn or not
        /// </summary>
        private bool mCanSpawnEnemies = true;

        /// <summary>
        /// A list of all spawned enemy tanks
        /// </summary>
        private readonly List<GameObject> mSpawnedEnemyTanks = new List<GameObject>();

        /// <summary>
        /// This is an internal counter for how many tanks we have spawned.
        /// </summary>
        private int mEnemyTanksSpawnedCount;

        /// <summary>
        /// This is a count of how many enemy tanks there are left to destroy. This is not the same as how many are active on screen.
        /// </summary>
        private int mEnemyTanksLeftCount;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise the Enemy Manager
            Init();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when the level is started
        /// </summary>
        public void OnLevelStarted()
        {
            // Initialise again
            Init();

            // Start spawning enemy tanks
            StartSpawningEnemies();
        }

        /// <summary>
        /// Removes an enemy tank from the spawned list
        /// </summary>
        /// <param name="go"></param>
        public void RemoveEnemyTank(GameObject go)
        {
            // Removes an enemy tank from the spawned list
            mSpawnedEnemyTanks.Remove(go);

            // Update our count
            mEnemyTanksLeftCount--;

            // Ensure we do not go below 0
            mEnemyTanksLeftCount = Mathf.Max(mEnemyTanksLeftCount, 0);

            // Fire an event with the updated enemy tank count
            FireEnemyTankCountUpdatedEvent();

            if (mEnemyTanksLeftCount <= 0)
            {
                // Stop spawning enemies
                StopSpawningEnemies();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises the EnemyManager
        /// </summary>
        private void Init()
        {
            // Set our tank count
            mEnemyTanksLeftCount = MaxEnemyTanks;
            FireEnemyTankCountUpdatedEvent();

            // Set our spawn count
            mEnemyTanksSpawnedCount = 0;
        }

        /// <summary>
        /// Starts spawning the enemy tanks
        /// </summary>
        private void StartSpawningEnemies()
        {
            // Start the spawning routine
            mSpawnEnemiesRoutine = SpawnEnemies();
            StartCoroutine(mSpawnEnemiesRoutine);
        }

        /// <summary>
        /// Routine which handles spawning enemies
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemies()
        {
            // Perform our initial spawn delay
            yield return new WaitForSeconds(InitialSpawnDelay);

            // Do our initial spawn
            SpawnSingleEnemy();

            // Ensure we can spawn enemies
            while (mCanSpawnEnemies)
            {
                // Wait for a duration before spawning another enemy tank
                yield return new WaitForSeconds(DelayBetweenSpawns);

                // Check if we have reached our max limit of tanks
                if (mEnemyTanksSpawnedCount < MaxEnemyTanks)
                {
                    // Spawn an enemy tank
                    SpawnSingleEnemy();
                }
            }
        }

        /// <summary>
        /// Spawns a single enemy tank
        /// </summary>
        private void SpawnSingleEnemy()
        {
            // Get a random spawn point from our list
            int randomSpawnPointIndex = Random.Range(0, SpawnPoints.Count);

            // Spawn the enemy at the spawn location
            GameObject enemyTank = Pooling.GetFromPool(EnemyTankPrefab, SpawnPoints[randomSpawnPointIndex].position, Quaternion.identity);

            // Add the enemy into the list
            mSpawnedEnemyTanks.Add(enemyTank);

            // Update our internal counter
            mEnemyTanksSpawnedCount++;
        }

        /// <summary>
        /// Stops enemies spawning
        /// </summary>
        private void StopSpawningEnemies()
        {
            // Set our flag to stop spawning enemies
            mCanSpawnEnemies = false;

            // Stop the routine
            StopCoroutine(mSpawnEnemiesRoutine);
            mSpawnEnemiesRoutine = null;

            // Fire an event that all have died
            FireAllTanksKilledEvent();
        }

        /// <summary>
        /// Fires the enemy tank count updated event
        /// </summary>
        private void FireEnemyTankCountUpdatedEvent()
        {
            OnEnemyTankCountUpdated?.Invoke(mEnemyTanksLeftCount);
        }

        /// <summary>
        /// Fires an event that all enemy tanks have been killed
        /// </summary>
        private void FireAllTanksKilledEvent()
        {
            OnAllEnemyTanksKilled?.Invoke();
        }

        #endregion
    }
}