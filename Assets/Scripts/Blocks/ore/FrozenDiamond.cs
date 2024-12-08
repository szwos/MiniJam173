namespace DefaultNamespace
{
    public class FrozenDiamond : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 1000;
        public FrozenDiamond(BlockId id) : base(id, "Frozen Diamond")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 0.3f;
    }
}