namespace DefaultNamespace
{
    
    public class Lead : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 150;
        public Lead(BlockId id) : base(id, "Lead")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 0.6f;
    }
}