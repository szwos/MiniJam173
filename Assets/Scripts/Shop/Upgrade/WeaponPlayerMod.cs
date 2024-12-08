using UnityEngine;

namespace DefaultNamespace.Upgrade
{
    public class WeaponPlayerMod : PlayerModBase
    {
        public float damageMultiplier = 1f;
        public float fireRateMultiplier = 1f;
        public override void Apply()
        {
            PlayerStats playerStats = Singleton<PlayerStats>.Instance;
            playerStats.DamageMultiplier = damageMultiplier;
            playerStats.FireRateMultiplier = fireRateMultiplier;
        }
    }
}