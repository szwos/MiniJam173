﻿namespace DefaultNamespace
{
    public class SecondBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public SecondBiome(BiomeId id) : base(id, "Second biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Copper, 0.97f, 0.15f));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Gold, 0.85f, 0.2f));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.Deepstone;
        }
    }
}