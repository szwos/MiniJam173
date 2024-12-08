namespace DefaultNamespace
{
    public class Stone : BlockBase, IDestroyableBlock
    {
        public Stone(BlockId id) : base(id, "Stone") { }

        public int Hardness { get; } = 2;
        public float MiningSpeedMultiplier { get; } = 1f;
    }
}