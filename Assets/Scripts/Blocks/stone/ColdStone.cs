namespace DefaultNamespace
{
    public class ColdStone : BlockBase, IDestroyableBlock
    {
        public ColdStone(BlockId id) : base(id, "ColdStone") { }
        
        public int Hardness { get; } = 5;
        public float MiningSpeedMultiplier { get; } = 0.3f;
    }
}