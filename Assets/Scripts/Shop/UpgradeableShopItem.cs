using System;
using DefaultNamespace.Upgrade;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    // [CreateAssetMenu(menuName = "Item/UpgradeableShopItem")]
    public class UpgradeableShopItem : MonoBehaviour
    {
        public ShopItem[] shopItems;
        public int id = 0;
        
        public ShopItem Current => id >= shopItems.Length ? shopItems[^1] : shopItems[id];

        public bool IsMax => id == shopItems.Length;
    }
}