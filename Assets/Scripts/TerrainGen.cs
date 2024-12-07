using System.Collections.Generic;

namespace DefaultNamespace
{
    public class TerrainGen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public BlockId[,] Generate()
        {
            BlockId[,] data = new BlockId[Width, Height];
            
            FillPhase(data);
            ApplyOreGen(data, new OreGenerator(BlockRegistry.Ore));
            
            return data;
        }

        private static void FillPhase(BlockId[,] data)
        {
            BiomeGenerator biomeGenerator = new BiomeGenerator(new List<BiomeRange>
            {
                new(BiomeRegistry.First, 0, 100),
                new(BiomeRegistry.Second, 101, 200)
            }, 10);
            
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = 0; y < data.GetLength(1); y++)
                {
                    data[x, y] = BiomeRegistry.Biomes[biomeGenerator.GetBiomeForPos(x, y)].GetBlock(x, y);
                }
            }
        }

        private static void ApplyOreGen(BlockId[,] data, OreGenerator gen)
        {
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = 0; y < data.GetLength(1); y++)
                {
                    var generated = gen.GenerateOre(x, y);
                    if (generated.HasValue)
                        data[x, y] = generated.Value;
                }
            }
        }
    }
}