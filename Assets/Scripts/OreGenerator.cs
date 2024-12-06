using System;

namespace DefaultNamespace
{
    public class OreGenerator
    {
        private BlockId _ore;
        private readonly FastNoiseLite _noise;
        private readonly float _treshold;
        
        public OreGenerator(BlockId ore, float treshold = 0.005f)
        {
            _noise = new FastNoiseLite();
            _noise.SetSeed(123);
            _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            _noise.SetFrequency(0.05f);
            
            _treshold = treshold;
        }

        public BlockId? GenerateOre(int x, int y)
        {
            return _noise.GetNoise(x, y) > _treshold ? _ore : null;
        }
    }
}