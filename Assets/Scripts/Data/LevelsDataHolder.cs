using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    [CreateAssetMenu(menuName = "Data/New Level Data")]
    public class LevelsDataHolder : ScriptableObject
    {
        #region Structs

        /// <summary>
        /// Level info
        /// </summary>
        [Serializable]
        public struct LevelInfo
        {
            /// <summary>
            /// The tilemap used for this level
            /// </summary>
            public GameObject LevelTilemap;

            /// <summary>
            /// The enemy prefab used for this level
            /// </summary>
            public GameObject EnemyPrefab;
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// List of levels
        /// </summary>
        public List<LevelInfo> Levels;

        #endregion
    }
}