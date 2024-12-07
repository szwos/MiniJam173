using UnityEngine;

namespace DefaultNamespace
{
    public class BlockBase
    {
        protected BlockId _id;
        protected string _name;

        public BlockId Id => _id;
        public string Name => _name;

        public BlockBase(BlockId id, string name)
        {
            _id = id;
            _name = name;
        }

        public bool CanDestroy()
        {
            return true; //This is a mock, Derived block will implement this, and this method will be virtual here
        }

    }
}