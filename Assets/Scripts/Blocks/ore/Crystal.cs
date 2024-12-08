namespace DefaultNamespace
{
    public class Crystal : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 600;
        public Crystal(BlockId id) : base(id, "Crystal")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 0.4f;
    }
}