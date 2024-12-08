using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class TerrainGen
    {
        public int FirstLayerHeight { get; set; } = 20;
        public int Width { get; set; }
        public int Height { get; set; }
        
        public BlockId[,] Generate(Tilemap tilemap, TileBase dirt)
        {
            BlockId[,] data = new BlockId[Width, Height];

            TerrainInitDetector.FillBlockIdMap(data, tilemap, Width, FirstLayerHeight, dirt, BlockRegistry.Dirt);
            
            FillPhase(data, FirstLayerHeight);
            // ApplyOreGen(data, new OreGenerator(BlockRegistry.Copper));
            
            return data;
        }

        private static void FillPhase(BlockId[,] data, int firstLayer)
        {
            BiomeGenerator biomeGenerator = new BiomeGenerator(new List<BiomeRange>
            {
                new BiomeRange(BiomeRegistry.GrassBiome, 0, 50),
                new(BiomeRegistry.First, 50, 100),
                new(BiomeRegistry.Second, 101, 200)
            }, 10);
            
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = firstLayer; y < data.GetLength(1); y++)
                {
                    data[x, y] = BiomeRegistry.Biomes[biomeGenerator.GetBiomeForPos(x, y)].GetBlock(x, y);
                }
            }
        }

        // private static void ApplyOreGen(BlockId[,] data, OreGenerator gen)
        // {
        //     for (var x = 0; x < data.GetLength(0); x++)
        //     {
        //         for (var y = 0; y < data.GetLength(1); y++)
        //         {
        //             var generated = gen.GenerateOre(x, y);
        //             if (generated.HasValue)
        //                 data[x, y] = generated.Value;
        //         }
        //     }
        // }
    }
}