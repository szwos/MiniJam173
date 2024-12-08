using UnityEngine;

namespace DefaultNamespace.Upgrade
{
    public class MovementSpeedPlayerMod : PlayerModBase
    {
        public float movementForceMultiplier;
        public override void Apply()
        {
            PlayerStats playerStats = Singleton<PlayerStats>.Instance;
            playerStats.MovementForceMultiplier = movementForceMultiplier;
        }
    }
}