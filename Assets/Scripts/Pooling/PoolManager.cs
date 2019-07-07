using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion
{
    public class PoolManager : MonoBehaviour
    {
        #region Structs

        /// <summary>
        /// Poolable item info
        /// </summary>
        [System.Serializable]
        public struct PoolItemInfo
        {
            /// <summary>
            /// The item to pool
            /// </summary>
            public GameObject PoolItem;

            /// <summary>
            /// The amount to preload
            /// </summary>
            public int PreloadQuantity;
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// List of items to preload, along with quantities
        /// </summary>
        public List<PoolItemInfo> PoolItemsToPreload;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Preload our pools
            PreloadPools();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Pre-loads our pools
        /// </summary>
        private void PreloadPools()
        {
            // Loop through all items we want to pre-load
            foreach (PoolItemInfo poolItemInfo in PoolItemsToPreload)
            {
                // Preload the item
                Pooling.Preload(poolItemInfo.PoolItem, poolItemInfo.PreloadQuantity);
            }
        }

        #endregion
    }
}