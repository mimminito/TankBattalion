using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    public class TerrainController : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Starting health for destructible tiles
        /// </summary>
        public int DestructibleTileStartingHealth = 30;

        #endregion

        #region Private Variables

        /// <summary>
        /// The terrain tilemap
        /// </summary>
        private Tilemap mTerrainTilemap;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Initialise
            Init();
        }

        private void Start()
        {
            // Setup the terrain
            SetupTerrain();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise
        /// </summary>
        private void Init()
        {
            mTerrainTilemap = GetComponent<Tilemap>();
        }

        /// <summary>
        /// Performs the terrain setup
        /// </summary>
        private void SetupTerrain()
        {
            // Loop through all possible tile positions
            foreach (Vector3Int tilePos in mTerrainTilemap.cellBounds.allPositionsWithin)
            {
                TileBase tileBase = mTerrainTilemap.GetTile(tilePos);
                if (tileBase == null)
                {
                    continue;
                }

                // Check it is destructible
                if (tileBase is DestructibleTile)
                {
                    // Check to see if we have a DestructibleTile
                    DestructibleTile tile = Instantiate(tileBase) as DestructibleTile;
                    if (tile == null)
                    {
                        continue;
                    }
                    
                    // Initialise the tile with our tilemap, its position and its starting health
                    tile.Init(mTerrainTilemap, tilePos, DestructibleTileStartingHealth);
                    mTerrainTilemap.SetTile(tilePos, tile);
                }
            }
        }

        #endregion
    }
}