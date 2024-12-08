namespace DefaultNamespace
{
    public class FirstBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public FirstBiome(BiomeId id) : base(id, "First biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Copper, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Iron, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Lead, OreRarity.RARE));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.Stone;
        }
    }
}