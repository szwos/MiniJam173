using UnityEngine;

namespace DefaultNamespace.Upgrade
{
    public class GunAccessPlayerMod : PlayerModBase
    {
        public GameObject Weapon;
        public GameObject WeaponTop;
        public override void Apply()
        {
            Debug.Log("Enable weapon");
            Weapon.SetActive(true);
            WeaponTop.SetActive(true);
        }
    }
}