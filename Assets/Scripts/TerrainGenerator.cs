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
    
    private Dictionary<BlockId, TileBase> blocks = new();
    
    void Start()
    {
        blocks.Add(BlockRegistry.Stone, stone);
        blocks.Add(BlockRegistry.Ore, ore);
        
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
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var id = mapData[x, y];
                
                if (blocks.TryGetValue(id, out TileBase tile))
                {
                    tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
    }
}
