namespace DefaultNamespace
{
    public class ColdestStone : BlockBase, IDestroyableBlock
    {
        public ColdestStone(BlockId id) : base(id, "ColdestStone") { }

        public int Hardness { get; } = 5;
        public float MiningSpeedMultiplier { get; } = 0.4f;
    }

}