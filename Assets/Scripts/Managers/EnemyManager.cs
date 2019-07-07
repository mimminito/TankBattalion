using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    public class EnemyManager : MonoBehaviour
    {
        #region Public Variables

        [Header("Enemy Tank")] public GameObject EnemyTankPrefab;

        [Header("Spawning")] public List<Transform> SpawnPoints;
        public float InitialSpawnDelay = 2f;
        public float SpawnDelay = 5f;
        public int MaxEnemyTanks = 5;

        #endregion

        #region Private Variables

        private IEnumerator mSpawnEnemiesRoutine;
        private bool mCanSpawnEnemies = true;
        private List<GameObject> mSpawnedEnemyTanks = new List<GameObject>();

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Start the spawning routine
            mSpawnEnemiesRoutine = SpawnEnemies();
            StartCoroutine(mSpawnEnemiesRoutine);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes an enemy tank from the spawned list
        /// </summary>
        /// <param name="go"></param>
        public void RemoveEnemyTank(GameObject go)
        {
            // Removes an enemy tank from the spawned list
            mSpawnedEnemyTanks.Remove(go);
        }

        #endregion

        #region Private Methods

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
                yield return new WaitForSeconds(SpawnDelay);

                // Check if we have reached our max limit of tanks
                if (mSpawnedEnemyTanks.Count < MaxEnemyTanks)
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
        }

        #endregion
    }
}