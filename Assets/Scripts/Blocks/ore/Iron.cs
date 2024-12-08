namespace DefaultNamespace
{
    public class Iron : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 200;
        public Iron(BlockId id) : base(id, "Iron")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 2;
        public float MiningSpeedMultiplier { get; } = 0.9f;
    }
}