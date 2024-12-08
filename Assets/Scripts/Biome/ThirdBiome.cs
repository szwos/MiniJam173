namespace DefaultNamespace
{
    public class ThirdBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public ThirdBiome(BiomeId id) : base(id, "Third biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Iron, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Lead, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Gold, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Emerald, OreRarity.SUPER_RARE));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.ColdStone;
        }
    }
}