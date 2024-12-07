namespace DefaultNamespace
{
    public class FirstBiome : BiomeBase
    {
        public FirstBiome(BiomeId id) : base(id, "First biome")
        {
            
        }

        public override BlockId GetBlock(int x, int y)
        {
            return BlockRegistry.Stone;
        }
    }
}