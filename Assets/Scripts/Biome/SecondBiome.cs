namespace DefaultNamespace
{
    public class SecondBiome : BiomeBase
    {
        private OreGenerator _copperOreGenerator;
        private OreGenerator _goldOreGenerator;
        public SecondBiome(BiomeId id) : base(id, "Second biome")
        {
            _copperOreGenerator = new OreGenerator(BlockRegistry.Copper, 0.97f, 0.15f);
            _goldOreGenerator = new OreGenerator(BlockRegistry.Gold, 0.85f, 0.2f);
        }

        public override BlockId GetBlock(int x, int y)
        {
            var generated = _goldOreGenerator.GenerateOre(x, y);
            if (generated.HasValue)
                return generated.Value;
            
            generated = _copperOreGenerator.GenerateOre(x, y);
            if (generated.HasValue)
                return generated.Value;
            
            return BlockRegistry.CobbleStone;
        }
    }
}