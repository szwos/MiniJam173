using System;
using DefaultNamespace.Shop;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.Shop
{
    public class ShopScript : MonoBehaviour
    {
        public UpgradeableShopItem[] shopItems;

        public UIDocument uiDocument;
        public VisualTreeAsset itemTemplate;
        public VisualTreeAsset shopTemplate;
        
        
        private VisualElement root;
        private VisualElement shopContainer;
        private VisualElement itemsContainer;
        
        void Start()
        {
            root = uiDocument.rootVisualElement;
            shopContainer = root.Q("shop-container");
            shopContainer.Add( shopTemplate.Instantiate());
            itemsContainer = shopContainer.Q<VisualElement>("Items");
            
            shopContainer.style.display = DisplayStyle.None;
            
            // GenerateShopItems();
        }

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
            // uiDocument.enabled = true;
            
            shopContainer.style.display = DisplayStyle.Flex;
            
            GenerateShopItems();
        }

        public void HideShop()
        {
            // uiDocument.enabled = false;
            shopContainer.style.display = DisplayStyle.None;
        }
        
        public void BuyItem(UpgradeableShopItem item)
        {
            Debug.Log($"BuyItem {item}");

            item.Current.PlayerMod.Apply();
            item.id++;
            
            GenerateShopItems();
        }

        void GenerateShopItems()
        {
            itemsContainer.Clear();
            foreach (var shopItem in shopItems)
            {
                ShopItem current = shopItem.Current;
                
                VisualElement itemElement = itemTemplate.Instantiate();
                // Debug.Log($"Creating {itemElement}");
                
                var icon = itemElement.Q<VisualElement>("Icon");
                icon.style.backgroundImage = new StyleBackground(current.icon);
                
                var label = itemElement.Q<Label>("ItemLabel");
                label.text = !shopItem.IsMax ? $"{current.itemName}\nPrice: {current.price}" : "MAX\n";
                
                var button = itemElement.Q<Button>("BuyButton");
                button.text = "Buy";
                
                if (!shopItem.IsMax)
                    button.clicked += () => BuyItem(shopItem);
                else button.SetEnabled(false);
                
                itemsContainer.Add(itemElement);
            }
            
            // Debug.Log($"Created {itemsContainer}");
            // Debug.Log($"Showing {itemsContainer.childCount} children");
        }
    }
}