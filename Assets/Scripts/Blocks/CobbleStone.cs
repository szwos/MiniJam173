namespace DefaultNamespace
{
    public class CobbleStone : BlockBase
    {
        public CobbleStone(BlockId id) : base(id, "CobbleStone") { }
        
        public bool canDestry { get; set; }
    }
}