namespace DefaultNamespace
{
    public class ColderStone : BlockBase, IDestroyableBlock
    {
        public ColderStone(BlockId id) : base(id, "ColderStone") { }
        
        public int Hardness { get; } = 6;
        public float MiningSpeedMultiplier { get; } = 0.4f;
    }
}