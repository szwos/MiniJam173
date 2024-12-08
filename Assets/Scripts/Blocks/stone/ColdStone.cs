namespace DefaultNamespace
{
    public class ColdStone : BlockBase, IDestroyableBlock
    {
        public ColdStone(BlockId id) : base(id, "ColdStone") { }
        
        public int Hardness { get; } = 4;
        public float MiningSpeedMultiplier { get; } = 0.3f;
    }
}