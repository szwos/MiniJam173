using System;

namespace DefaultNamespace
{
    public struct BlockId : IEquatable<BlockId>
    {
        public byte Value { get; set; }
        
        public BlockId(byte value)
        {
            Value = value;
        }

        public bool Equals(BlockId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is BlockId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}