using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{

    public TerrainMananger TerrainMananger;

    public int Width = 100;
    public int Height = 200;

    private Dictionary<BlockId, TileBase> blocks = new();
    
    void Start()
    {        
        BlockId[,] mapData = GenerateMapData();
        TerrainMananger.Init(mapData);

    }

    BlockId[,] GenerateMapData()
    {
        TerrainGen terrainGen = new()
        {
            Width = Width,
            Height = Height,
        };
        return terrainGen.Generate();
    }
}
