namespace DefaultNamespace
{
    public class Platinium : BlockBase, IGiveMoneyBlock, IDestroyableBlock
    {
        private int _money = 400;
        public Platinium(BlockId id) : base(id, "Platinium")
        {
        }

        public int Money => _money;
        public int Hardness { get; } = 3;
        public float MiningSpeedMultiplier { get; } = 0.8f;
    }
}