using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public static class TerrainInitDetector
    {
        public static void FillBlockIdMap(BlockId[,] blockIdMap, Tilemap tilemap, int width, int height, TileBase dirtTile, BlockId dirt)
        {
            // Get the origin position of the Tilemap in world space
            Vector3 worldStartPosition = tilemap.transform.position;

            // Loop through each grid cell within the specified width and height
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Calculate the world position for the current tile
                    Vector3 worldPosition = worldStartPosition + new Vector3(x, -y, 0);

                    // Convert world position to tilemap cell position
                    Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

                    // Retrieve the tile at the cell position
                    TileBase currentTile = tilemap.GetTile(cellPosition);
                    // Check if the tile matches the given dirtTile
                    if (currentTile == dirtTile)
                    {
                        blockIdMap[x, y] = dirt; // Assign the corresponding BlockId
                    }
                }
            }
        }
    }
}