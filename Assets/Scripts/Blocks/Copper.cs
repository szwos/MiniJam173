namespace DefaultNamespace
{
    public class Copper : BlockBase, IGiveMoneyBlock
    {
        private int _money = 100;
        public Copper(BlockId id) : base(id, "Copper")
        {
        }

        public int Money => _money;
    }
}