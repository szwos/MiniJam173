namespace DefaultNamespace
{
    public class Gold : BlockBase, IGiveMoneyBlock
    {
        private int _money = 500;
        public Gold(BlockId id) : base(id, "Gold")
        {
        }

        public int Money => _money;
    }
}