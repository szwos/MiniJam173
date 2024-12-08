namespace DefaultNamespace
{
    public class SecondBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public SecondBiome(BiomeId id) : base(id, "Second biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Copper, OreRarity.SUPER_RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Iron, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Lead, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Gold, OreRarity.RARE));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.Deepstone;
        }
    }
}