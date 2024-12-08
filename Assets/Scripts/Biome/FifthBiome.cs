namespace DefaultNamespace
{
    public class FifthBiome : BiomeBase
    {
        private readonly OreGeneratorChain _oreGeneratorChain;
        public FifthBiome(BiomeId id) : base(id, "Third biome")
        {
            _oreGeneratorChain = new OreGeneratorChain();
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Emerald, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Platinium, OreRarity.COMMON));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.Crystal, OreRarity.RARE));
            _oreGeneratorChain.AddGenerator(new OreGenerator(BlockRegistry.FrozenDiamond, OreRarity.SUPER_RARE));
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _oreGeneratorChain.GenerateOre(x, y);
            return generated ?? BlockRegistry.ColdestStone;
        }
    }
}