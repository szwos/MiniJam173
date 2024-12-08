using DefaultNamespace.Upgrade;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    // [CreateAssetMenu(menuName = "Item/UpgradeableShopItem")]
    public class UpgradeableShopItem : MonoBehaviour
    {
        public ShopItem[] shopItems;
        public int id = 0;
        
        public ShopItem Current => shopItems[id];
    }
}