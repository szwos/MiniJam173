namespace DefaultNamespace
{
    public class Copper : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 100;
        public Copper(BlockId id) : base(id, "Copper")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 1;
        public float MiningSpeedMultiplier { get; } = 0.7f;
    }
}