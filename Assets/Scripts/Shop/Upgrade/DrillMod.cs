namespace DefaultNamespace.Upgrade
{
    public class DrillMod : PlayerModBase
    {
        public int hardness = 1;
        public float miningSpeedMultiplier = 1f;
        public override void Apply()
        {
            PlayerStats playerStats = Singleton<PlayerStats>.Instance;
            playerStats.DrillHardness = hardness;
            playerStats.DrillSpeedMultiplier = miningSpeedMultiplier;
        }
    }
}