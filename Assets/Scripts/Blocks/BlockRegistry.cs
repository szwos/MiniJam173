﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DefaultNamespace
{
    public static class BlockRegistry
    {
        public static readonly BlockId Air;
        public static readonly BlockId Stone;
        public static readonly BlockId Copper;
        public static readonly BlockId Gold;
        public static readonly BlockId CobbleStone;
        public static readonly BlockId Dirt;

        static BlockRegistry()
        {
            Air = Register(0);
            Stone = Register(id => new Stone(id));
            Copper = Register(id => new Copper(id));
            Gold = Register(id => new Gold(id));
            CobbleStone = Register(id => new Deepstone(id));
            Dirt = Register(id => new DirtBlock(id));
        }

        [ItemCanBeNull] public static Dictionary<BlockId, BlockBase> Blocks { get; } = new();

        private static byte _id = 0;

        public static BlockId Register(byte id)
        {
            var blockId = new BlockId(id);
            // Blocks[blockId] = null;
            return blockId;
        }
        
        public static BlockId Register(Func<BlockId, BlockBase> func)
        {
            _id++;
            var blockId = new BlockId(_id);
            Blocks[blockId] = func(blockId);
            return blockId;
        }
    }
}