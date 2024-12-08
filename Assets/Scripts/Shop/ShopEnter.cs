using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    public class ShopEnter : MonoBehaviour
    {
        public ShopScript CurrentShop { get; set; }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(CurrentShop);
                
                if(CurrentShop != null)
                    CurrentShop.ShowShop();
            } else if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentShop.HideShop();
            }
        }
    }
}