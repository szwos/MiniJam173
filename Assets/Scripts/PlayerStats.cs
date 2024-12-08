using System.Collections.Generic;
using DefaultNamespace.Upgrade;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    //Movement
    public float MovementForceMultiplier { get; set; } = 1f;


    //Fuel
    public float MaxFuel { get; set; } = 1000;
    public float Fuel { get; set; } = 1000;
    public float FuelConsumption { get; set; } = 1f;
    
    //Health
    public float MaxHealth { get; set; } = 100;
    public float Health { get; set; } = 100;        

    //Weapon
    public float DamageMultiplier { get; set; } = 1f;
    public float FireRateMultiplier { get; set; } = 1f;
    public float BulletSpeedMultiplier { get; set; } = 1f;
    public WeaponPlayerMod WeaponPlayerMod { get; set; }

    //Drill
    public int DrillHardness { get; set; } = 1;
    public float DrillSpeedMultiplier { get; set; } = 1f;

    //Economy
    public int Money { get; set; }

    //Misc
    public int Depth { get; set; }

}
       


