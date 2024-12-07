namespace DefaultNamespace
{
    public class SecondBiome : BiomeBase
    {
        public SecondBiome(BiomeId id) : base(id, "Second biome")
        {
            
        }

        public override BlockId GetBlock(int x, int y)
        {
            return BlockRegistry.CobbleStone;
        }
    }
}