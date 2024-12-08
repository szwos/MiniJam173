namespace DefaultNamespace
{
    public class Gold : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 500;
        public Gold(BlockId id) : base(id, "Gold")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 0.7f;
    }
}