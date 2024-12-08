using System;
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
        public static readonly BlockId Deepstone;
        public static readonly BlockId DeeperStone;
        public static readonly BlockId Dirt;

        
        public static readonly BlockId Iron;
        public static readonly BlockId Lead;
        public static readonly BlockId Emerald;
        public static readonly BlockId Platinium;
        public static readonly BlockId Crystal;
        public static readonly BlockId FrozenDiamond;
        
        public static readonly BlockId ColdStone;
        public static readonly BlockId ColderStone;
        public static readonly BlockId ColdestStone;
        static BlockRegistry()
        {
            Air = Register(0);
            Stone = Register(id => new Stone(id));
            Copper = Register(id => new Copper(id));
            Gold = Register(id => new Gold(id));
            Deepstone = Register(id => new Deepstone(id));
            DeeperStone = Register(id => new DeeperStone(id));
            Dirt = Register(id => new DirtBlock(id));
            
            Iron = Register(id => new Iron(id));
            Lead = Register(id => new Lead(id));
            Emerald = Register(id => new Emerald(id));
            Platinium = Register(id => new Platinium(id));
            Crystal = Register(id => new Crystal(id));
            FrozenDiamond = Register(id => new FrozenDiamond(id));
            
            ColdStone = Register(id => new ColdStone(id));
            ColderStone = Register(id => new ColderStone(id));
            ColdestStone = Register(id => new ColdestStone(id));
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