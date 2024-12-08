namespace DefaultNamespace
{
    public class Emerald : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 500;
        public Emerald(BlockId id) : base(id, "Emerald")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 4;
        public float MiningSpeedMultiplier { get; } = 0.5f;
    }
}