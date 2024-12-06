namespace DefaultNamespace
{
    public class Stone : BlockBase
    {
        public Stone(BlockId id) : base(id, "Stone") { }
        
        public bool canDestry { get; set; }
    }
}