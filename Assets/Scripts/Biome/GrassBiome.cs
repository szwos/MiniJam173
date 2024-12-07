using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class GrassBiome : BiomeBase
    {
        private BlockId _grassBlock;
        private BlockId _airBlock;
        public GrassBiome(BiomeId id) : base(id, "Grass biome")
        {
            _grassBlock = BlockRegistry.Dirt;
            _airBlock = BlockRegistry.Air;
        }
        
        

        public override BlockId GetBlock(int x, int y)
        {
            return y > 5 ? _grassBlock : _airBlock;
        }
    }
}