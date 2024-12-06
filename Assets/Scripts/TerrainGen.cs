namespace DefaultNamespace
{
    public class TerrainGen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public BlockId[,] Generate()
        {
            BlockId[,] data = new BlockId[Width, Height];

            Fill(data);
            ApplyOreGen(data, new OreGenerator(BlockRegistry.Ore));
            
            return data;
        }

        private static void Fill(BlockId[,] data)
        {
            var blockId = BlockRegistry.Stone;
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = 0; y < data.GetLength(1); y++)
                {
                    data[x, y] = blockId;
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