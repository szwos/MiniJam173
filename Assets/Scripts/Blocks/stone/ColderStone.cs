namespace DefaultNamespace
{
    public class ColderStone : BlockBase, IDestroyableBlock
    {
        public ColderStone(BlockId id) : base(id, "ColderStone") { }
        
        public int Hardness { get; } = 5;
        public float MiningSpeedMultiplier { get; } = 0.2f;
    }
}