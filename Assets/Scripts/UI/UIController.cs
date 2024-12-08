using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text MoneyDisplay;
    public Text DepthDisplay;
    public Slider FuelDisplay;
    public Slider HealthDisplay;

    public float fuel;
    public float actualFuel;

    private void Update()
    {
        MoneyDisplay.text = PlayerStats.Instance.Money.ToString() + " $";
        
        FuelDisplay.value = 1 - PlayerStats.Instance.Fuel / PlayerStats.Instance.MaxFuel;

        HealthDisplay.value = 1 - PlayerStats.Instance.Health / PlayerStats.Instance.MaxHealth;

        DepthDisplay.text = PlayerStats.Instance.Depth.ToString() + " meters";
    }
}
