namespace DefaultNamespace
{
    public class Stone : BlockBase, IDestroyableBlock
    {
        public Stone(BlockId id) : base(id, "Stone") { }

        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 1f;
    }
}