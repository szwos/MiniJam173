namespace DefaultNamespace
{
    public class DeeperStone : BlockBase, IDestroyableBlock
    {
        public DeeperStone(BlockId id) : base(id, "DeeperStone") { }
        
        public int Hardness { get; } = 4;
        public float MiningSpeedMultiplier { get; } = 0.5f;
    }
}