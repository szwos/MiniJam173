using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DefaultNamespace
{
    public static class BlockRegistry
    {
        public static BlockId Air = Register(0);
        
        public static BlockId Stone => Register(id => new Stone(id));
        public static BlockId Ore => Register(id => new Ore(id));
        
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