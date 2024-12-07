namespace DefaultNamespace
{
    public abstract class BiomeBase
    {
        protected BiomeId _id;
        protected string _name;

        public BiomeBase(BiomeId id, string name)
        {
            _id = id;
            _name = name;
        }

        public BiomeId ID => _id;

        public string Name => _name;

        public abstract BlockId GetBlock(int x, int y);
    }
}