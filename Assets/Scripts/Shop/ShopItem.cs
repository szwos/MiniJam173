using DefaultNamespace.Upgrade;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Shop
{
    // [CreateAssetMenu(menuName = "Item/ShopItem")]
    public class ShopItem : MonoBehaviour
    {
        public string itemName;
        public int price;
        public Sprite icon;
        private PlayerModBase _playerMod;
        
        public PlayerModBase PlayerMod => _playerMod;

        void Start()
        {
            _playerMod = GetComponent<PlayerModBase>();
        }
    }
}