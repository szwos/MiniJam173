using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    // public TileBase[] tiles;

    public TileBase stone;
    public TileBase ore;
    public TileBase cobble;
    
    private Dictionary<BlockId, TileBase> blocks = new();
    
    void Start()
    {
        blocks.Add(BlockRegistry.Stone, stone);
        blocks.Add(BlockRegistry.Ore, ore);
        blocks.Add(BlockRegistry.CobbleStone, cobble);
        
        Debug.Log(tilemap);
        BlockId[,] mapData = GenerateMapData();
        FillTilemap(mapData);
    }

    BlockId[,] GenerateMapData()
    {
        TerrainGen terrainGen = new()
        {
            Width = 256,
            Height = 512,
        };
        return terrainGen.Generate();
    }

    void FillTilemap(BlockId[,] mapData)
    {
        int width = mapData.GetLength(0);
        int height = mapData.GetLength(1);
        
        TileBase[] tiles = new TileBase[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = x + y * width;
                var id = mapData[x, y];

                if (blocks.TryGetValue(id, out TileBase tile))
                {
                    tiles[index] = tile;
                }
                else
                {
                    tiles[index] = null;
                }
            }
        }
        var bounds = new BoundsInt(0, -height + 1, 0, width, height, 1);
        tilemap.SetTilesBlock(bounds, tiles);
    }

}
