using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityTankBattalion
{
    internal static class TilemapUtils
    {
        /// <summary>
        /// Gets a cell at a specific position for a given tilemap
        /// </summary>
        /// <param name="tilemap"></param>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        public static TileBase GetTilemapCellAtPosition(Tilemap tilemap, Vector2 worldPosition)
        {
            return tilemap.GetTile(tilemap.WorldToCell(worldPosition));
        }
    }
}