using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DefaultNamespace
{
    public class BiomeRegistry
    {
        public static readonly BiomeId GrassBiome;
        public static readonly BiomeId First;
        public static readonly BiomeId Second;

        static BiomeRegistry()
        {
            GrassBiome = Register(id => new GrassBiome(id));
            First = Register(id => new FirstBiome(id));
            Second = Register(id => new SecondBiome(id));
        }

        public static Dictionary<BiomeId, BiomeBase> Biomes { get; } = new();

        private static byte _id = 0;
        public static BiomeId Register(Func<BiomeId, BiomeBase> func)
        {
            var biomeId = new BiomeId(_id);
            _id++;
            Biomes[biomeId] = func(biomeId);
            return biomeId;
        }
    }
}