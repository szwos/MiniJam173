using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text MoneyDisplay;


    private void Update()
    {
        MoneyDisplay.text = PlayerStats.Instance.Money.ToString() + " $";
    }
}
