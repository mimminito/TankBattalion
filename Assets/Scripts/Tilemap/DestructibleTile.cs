using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class DestructibleTile : Tile
    {
        #region Public Variables

        /// <summary>
        /// Prefab to spawn when this tile is destroyed
        /// </summary>
        public GameObject ExplosionPrefab;

        #endregion

        #region Private Variables

        /// <summary>
        /// Our current health
        /// </summary>
        private int mCurrentHealth;

        /// <summary>
        /// Our tilemap
        /// </summary>
        private Tilemap mTilemap;

        /// <summary>
        /// Our position
        /// </summary>
        private Vector3Int mTilePos;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialises this tile
        /// </summary>
        /// <param name="tilemap"></param>
        /// <param name="position"></param>
        /// <param name="startingHealth"></param>
        public void Init(Tilemap tilemap, Vector3Int position, int startingHealth)
        {
            mCurrentHealth = startingHealth;

            mTilemap = tilemap;
            mTilePos = position;
        }

        /// <summary>
        /// Performs damage to this tile
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            // Check if we are already dead
            if (mCurrentHealth <= 0)
            {
                return;
            }

            // Subtract the damage from our health
            mCurrentHealth -= damage;

            if (mCurrentHealth <= 0)
            {
                Kill();
            }
        }

        /// <summary>
        /// Kills this tile
        /// </summary>
        public void Kill()
        {
            // Set this tile to null so it is hidden
            mTilemap.SetTile(mTilePos, null);

            // Spawn the effect
            SpawnExplosionEffect();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Spawns an explosion effect
        /// </summary>
        private void SpawnExplosionEffect()
        {
            // Check we have an explosion prefab to spawn
            if (ExplosionPrefab == null)
            {
                return;
            }

            // Spawn at the center of the tile
            Pooling.GetFromPool(ExplosionPrefab, mTilemap.GetCellCenterWorld(mTilePos), Quaternion.identity);
        }

        #endregion

        #region Scriptable Object

        [MenuItem("Assets/Custom Tiles/Destructible Tile")]
        static void CreateTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save New Tile", "DestructibleTile", "Asset", "Save Tile");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DestructibleTile>(), path);
        }

        #endregion
    }
}