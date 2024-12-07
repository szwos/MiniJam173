using UnityEngine;
public class PlayerStats : Singleton<PlayerStats>
{
    public int Money { get; set; }
    public float MaxFuel { get; set; } = 1000;
    public float Fuel { get; set; } = 1000;
    
    public float FuelConsumption { get; set; } = 10;

}
       


