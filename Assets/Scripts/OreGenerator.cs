using System;

namespace DefaultNamespace
{
    public class OreGenerator
    {
        private BlockId _ore;
        private readonly FastNoiseLite _noise;
        private readonly float _treshold;
        
        public OreGenerator(BlockId ore, float treshold = 0.95f, float frequency = 0.05f)
        {
            _noise = new FastNoiseLite();
            //_noise.SetSeed(123); not today
            _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            _noise.SetFrequency(frequency);
            
            _ore = ore;
            _treshold = treshold;
        }

        public BlockId? GenerateOre(int x, int y)
        {
            return _noise.GetNoise(x, y) > _treshold ? _ore : null;
        }
    }
}