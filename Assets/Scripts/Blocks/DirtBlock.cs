namespace DefaultNamespace
{
    public class DirtBlock : BlockBase, IDestroyableBlock
    {
        public DirtBlock(BlockId id) : base(id, "Dirt block")
        {
        }

        public int Hardness { get; } = 0;
        public float MiningSpeedMultiplier { get; } = 2f;
    }
}