using System.Collections.Generic;

namespace DefaultNamespace
{
    public class OreGeneratorChain
    {
        private readonly List<OreGenerator> _oreGenerators;

        public OreGeneratorChain()
        {
            _oreGenerators = new List<OreGenerator>();
        }
        
        public void AddGenerator(OreGenerator generator)
        {
            _oreGenerators.Add(generator);
        }
        
        public BlockId? GenerateOre(int x, int y)
        {
            foreach (var generator in _oreGenerators)
            {
                var generated = generator.GenerateOre(x, y);
                if (generated.HasValue)
                    return generated.Value;
            }

            return null; // No ore generated
        }
    }
}