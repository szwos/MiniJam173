namespace DefaultNamespace
{
    public class Deepstone : BlockBase, IDestroyableBlock
    {
        public Deepstone(BlockId id) : base(id, "Deepstone") { }
        
        public int Hardness { get; } = 2;
        public float MiningSpeedMultiplier { get; } = 0.7f;
    }
}