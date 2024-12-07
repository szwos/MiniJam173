﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Unity.Collections.AllocatorManager;

namespace DefaultNamespace
{
    public class TerrainMananger : MonoBehaviour
    {
        public TileBase stone;
        public TileBase ore;
        public TileBase cobble;

        private Tilemap _tilemap;
        private BlockId[,] _blockMap;
        private Dictionary<BlockId, TileBase> _blockDictionary = new();

        public void Start()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
        }

        public void Init(BlockId[,] mapData)
        {
            _blockDictionary.Add(BlockRegistry.Stone, stone);
            _blockDictionary.Add(BlockRegistry.Ore, ore);
            _blockDictionary.Add(BlockRegistry.CobbleStone, cobble);

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

        public void SetBlock(Vector3Int position, BlockId blockId)
        {
            _tilemap.SetTile(position, _blockDictionary[blockId]);
        }

        public BlockBase GetBlock(Vector3 position)
        {
            Vector3Int cellCoords = _tilemap.WorldToCell(position);
            var blockId = _blockMap[cellCoords.x, cellCoords.y];
            return BlockRegistry.Blocks[blockId];
        }

    }
}