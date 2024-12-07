﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using static Unity.Collections.AllocatorManager;

namespace DefaultNamespace
{
    public class TerrainMananger : MonoBehaviour
    {
        public int Width = 100;
        public int Height = 200;
        
        public TileBase dirt;
        public TileBase stone;
        public TileBase copper;
        public TileBase gold;
        public TileBase cobble;

        private Tilemap _tilemap;
        private BlockId[,] _blockMap;
        private Dictionary<BlockId, TileBase> _blockDictionary = new();

        public void Start()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
            
            TerrainGen terrainGen = new()
            {
                Width = Width,
                Height = Height,
            };
            Init(terrainGen.Generate(_tilemap, dirt));
        }

        private void Init(BlockId[,] mapData)
        {
            _blockDictionary.Add(BlockRegistry.Dirt, dirt);
            _blockDictionary.Add(BlockRegistry.Stone, stone);
            _blockDictionary.Add(BlockRegistry.Copper, copper);
            _blockDictionary.Add(BlockRegistry.Gold, gold);
            _blockDictionary.Add(BlockRegistry.CobbleStone, cobble);
            _blockDictionary.Add(BlockRegistry.Air, null);

            _blockMap = mapData;
            FillTilemap(mapData);
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

                    if (_blockDictionary.TryGetValue(id, out TileBase tile))
                    {
                        _tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                    }
                }
            }
        }

        private Vector2Int GetBlockGridPosition(Vector3Int position)
        {
            return new Vector2Int(position.x, -position.y);
        }
        
        // public void SetBlock(Vector3Int position, BlockId blockId)
        // {
        //     _tilemap.SetTile(position, _blockDictionary[blockId]);
        // }
        

        public void SetBlock(Vector3 worldPosition, BlockId newBlockId)
        {
            Vector3Int cellCoords = _tilemap.WorldToCell(worldPosition);
            Vector2Int blockPosition = GetBlockGridPosition(cellCoords);
            _blockMap[blockPosition.x, blockPosition.y] = newBlockId;
            _tilemap.SetTile(cellCoords, _blockDictionary[newBlockId]);
        }

        public BlockBase? GetBlock(Vector3 worldPosition)
        {
            Vector3Int cellCoords = _tilemap.WorldToCell(worldPosition);
            Vector2Int blockPosition = GetBlockGridPosition(cellCoords);
            BlockId? blockId = null;
            try
            {
                blockId = _blockMap[blockPosition.x, blockPosition.y];
            } catch (IndexOutOfRangeException e)
            {
                Debug.Log(e.Message);
                return null;
            }
            
            return BlockRegistry.Blocks[blockId.GetValueOrDefault()];
        }

    }
}