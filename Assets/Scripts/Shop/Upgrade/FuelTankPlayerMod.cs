using DefaultNamespace.Upgrade;
using UnityEngine;

public class FuelTankPlayerMod : PlayerModBase
{
    public float NewFuel = 2000;
    public override void Apply()
    {
        PlayerStats.Instance.MaxFuel = NewFuel;
    }
}
