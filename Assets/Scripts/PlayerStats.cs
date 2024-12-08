using System.Collections.Generic;
using DefaultNamespace.Upgrade;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public int Money { get; set; }
    public float MaxFuel { get; set; } = 1000;
    public float Fuel { get; set; } = 1000;

    public float MaxHealth { get; set; } = 100;
    public float Health { get; set; } = 100;
    
    public float FuelConsumption { get; set; } = 1f;

    public WeaponPlayerMod WeaponPlayerMod { get; set; }

    public float DamageMultiplier { get; set; } = 1f;
    public float FireRateMultiplier { get; set; } = 10f;
    public float BulletSpeedMultiplier { get; set; } = 10f;
    public float MovementForceMultiplier { get; set; } = 1f;

}
       


