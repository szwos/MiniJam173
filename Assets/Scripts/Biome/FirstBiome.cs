namespace DefaultNamespace
{
    public class FirstBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public FirstBiome(BiomeId id) : base(id, "First biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Copper, 0.84f, 0.15f));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Gold, 0.93f, 0.2f));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.Stone;
        }
    }
}