using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class GrassBiome : BiomeBase
    {
        private BlockId _grassBlock;
        private BlockId _airBlock;
        private OreGenerator _oreGenerator;
        public GrassBiome(BiomeId id) : base(id, "Grass biome")
        {
            _grassBlock = BlockRegistry.Dirt;
            _airBlock = BlockRegistry.Air;

            _oreGenerator = new OreGenerator(BlockRegistry.Copper, 0.8f, 0.4f);
        }
        
        public override BlockId GetBlock(int x, int y)
        {
            BlockId baseBlock;
            if (y > 5)
            {
                baseBlock = _grassBlock;
                
                var generated = _oreGenerator.GenerateOre(x, y);
                if (generated.HasValue)
                    baseBlock = generated.Value;
            }
            else
                baseBlock = _airBlock;

            
            
            return baseBlock;
        }
    }
}