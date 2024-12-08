namespace DefaultNamespace
{
    public class FourthBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public FourthBiome(BiomeId id) : base(id, "Third biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Gold, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Emerald, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Platinium, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Crystal, OreRarity.SUPER_RARE));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.ColderStone;
        }
    }
}