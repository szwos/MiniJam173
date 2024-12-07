using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text MoneyDisplay;
    public Slider FuelDisplay;

    public float fuel;
    public float actualFuel;

    private void Update()
    {
        MoneyDisplay.text = PlayerStats.Instance.Money.ToString() + " $";
        FuelDisplay.value = 1 - PlayerStats.Instance.Fuel /PlayerStats.Instance.MaxFuel;
        fuel = 1 - PlayerStats.Instance.Fuel / PlayerStats.Instance.MaxFuel;
        actualFuel = PlayerStats.Instance.Fuel;
    }
}
