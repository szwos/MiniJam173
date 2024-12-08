using System;
using DefaultNamespace.Shop;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.Shop
{
    public class ShopScript : MonoBehaviour
    {
        public UpgradeableShopItem[] shopItems;

        public UIDocument uiDocument; // The main UIDocument in your scene.
        public VisualTreeAsset itemTemplate; // Directly assignable in the Inspector.

        private VisualElement itemsContainer;
        
        // void Start()
        // {
        //     GenerateShopItems();
        // }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log($"Enter {other}");
            var component = other.GetComponent<ShopEnter>();
            component.CurrentShop = this;
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log($"Exit {other}");
            var component = other.GetComponent<ShopEnter>();
            component.CurrentShop = null;
        }

        public void ShowShop()
        {
            uiDocument.enabled = true;
            GenerateShopItems();
        }

        public void HideShop()
        {
            uiDocument.enabled = false;
        }
        
        public void BuyItem(UpgradeableShopItem item)
        {
            Debug.Log($"BuyItem {item}");

            item.Current.PlayerMod.Apply();
            item.id++;
            
            
        }
        
        void GenerateShopItems()
        {
            var root = uiDocument.rootVisualElement;
            itemsContainer = root.Q<VisualElement>("Items");
            foreach (var shopItem in shopItems)
            {
                ShopItem current = shopItem.Current;
                
                // Clone the item template and bind data.
                VisualElement itemElement = itemTemplate.Instantiate();

                // Set up the item icon.
                var icon = itemElement.Q<VisualElement>("Icon");
                icon.style.backgroundImage = new StyleBackground(current.icon);

                // Set up the item name and price.
                var label = itemElement.Q<Label>("ItemLabel");
                label.text = $"{current.itemName}\nPrice: {current.price}";

                // Set up the buy button.
                var button = itemElement.Q<Button>("BuyButton");
                button.text = "Buy";
                button.clicked += () => BuyItem(shopItem);

                // Add the item element to the container.
                itemsContainer.Add(itemElement);
            }
        }
    }
}