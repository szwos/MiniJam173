using System;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public struct BiomeId : IEquatable<BiomeId>
    {
        public byte Value { get; set; }
            
        public BiomeId(byte value)
        {
            Value = value;
        }

        public bool Equals(BiomeId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is BiomeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}