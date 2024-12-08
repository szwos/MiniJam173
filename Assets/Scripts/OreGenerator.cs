using System;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class OreGenerator
    {
        private BlockId _ore;
        private readonly FastNoiseLite _noise;
        private readonly float _treshold;

        public OreGenerator(BlockId ore, OreRarity rarity): this(ore, rarity.Treshold, rarity.Frequency) 
        { }
        
        public OreGenerator(BlockId ore, float treshold = 0.95f, float frequency = 0.05f)
        {
            _noise = new FastNoiseLite();
            _noise.SetSeed(Random.Range(int.MinValue, int.MaxValue));
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
    
    public class OreRarity
    {
        public float Treshold;
        public float Frequency;

        public OreRarity(float treshold, float frequency)
        {
            Treshold = treshold;
            Frequency = frequency;
        }
        
        public static OreRarity SUPER_RARE = new(0.98f, 0.2f);
        public static OreRarity RARE = new(0.95f, 0.15f);
        public static OreRarity COMMON = new(0.87f, 0.15f);

        // Rare = (0.98f, 0.05f),
        // Normal = (0.9f, 0.5f),
    }
}