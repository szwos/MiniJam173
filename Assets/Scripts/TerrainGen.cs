namespace DefaultNamespace
{
    public class TerrainGen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public BlockId[,] Generate()
        {
            BlockId[,] data = new BlockId[Width, Height];

            fill(data);
            applyOreGen(data, new OreGenerator(BlockRegistry.Ore));
            
            return data;
        }

        private void fill(BlockId[,] data)
        {
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = 0; y < data.GetLength(1); y++)
                {
                    data[x, y] = BlockRegistry.Stone;
                }
            }
        }

        private void applyOreGen(BlockId[,] data, OreGenerator gen)
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