using UnityEngine;

namespace DefaultNamespace.Upgrade
{
    public class GunAccessPlayerMod : PlayerModBase
    {
        public GameObject Weapon;
        public GameObject WeaponTop;
        public override void Apply()
        {
            Weapon.SetActive(true);
            WeaponTop.SetActive(true);
        }
    }
}