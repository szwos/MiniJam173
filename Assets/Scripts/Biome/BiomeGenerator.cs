using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BiomeRange
    {
        public BiomeId Biome { get; }
        public int StartHeight { get; }
        public int EndHeight { get; }

        public BiomeRange(BiomeId biome, int startHeight, int endHeight)
        {
            Biome = biome;
            StartHeight = startHeight;
            EndHeight = endHeight;
        }
    }
    
    public class BiomeGenerator
    {
        private readonly List<BiomeRange> _biomes;
        private readonly int _gradientSize;

        public BiomeGenerator(List<BiomeRange> biomes, int gradientSize)
        {
            _biomes = biomes;
            _gradientSize = gradientSize;
        }

        public BiomeId GetBiomeForPos(int x, int y)
        {
            for (int i = 0; i < _biomes.Count; i++)
            {
                var current = _biomes[i];

                if (y < current.StartHeight)
                    continue;

                if (y <= current.EndHeight)
                {
                    // Inside the range for this biome
                    if (i + 1 < _biomes.Count && y >= current.EndHeight - _gradientSize)
                    {
                        // Smooth transition to next biome
                        var next = _biomes[i + 1];
                        double t = (double)(y - (current.EndHeight - _gradientSize)) / _gradientSize;
                        double probability = SmoothStep(t);

                        return Random.value < probability ? next.Biome : current.Biome;
                    }

                    return current.Biome;
                }
            }

            // Default biome if no range matches
            return _biomes[_biomes.Count - 1].Biome;
        }

        private double SmoothStep(double t)
        {
            // Smoothstep interpolation: 3t^2 - 2t^3
            return t * t * (3 - 2 * t);
        }
    }
}