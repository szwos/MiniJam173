namespace DefaultNamespace
{
    public interface IDestroyableBlock
    {
        public int Hardness { get; }
        public float MiningSpeedMultiplier { get; }
    }
}