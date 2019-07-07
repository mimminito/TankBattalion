using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    public class PlayerBaseManager : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The trophy prefab
        /// </summary>
        public GameObject BaseTrophy;

        /// <summary>
        /// The trophy spawn point
        /// </summary>
        public Transform SpawnPoint;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when the level starts
        /// </summary>
        public void OnLevelStart()
        {
            // Spawn the trophy
            Pooling.GetFromPool(BaseTrophy, SpawnPoint.position, Quaternion.identity);
        }

        #endregion
    }
}