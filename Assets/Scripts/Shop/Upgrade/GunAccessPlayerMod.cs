using UnityEngine;

namespace DefaultNamespace.Upgrade
{
    public class GunAccessPlayerMod : PlayerModBase
    {
        public GameObject weapon;
        public override void Apply()
        {
            Instantiate(weapon, GameObject.Find("Character").transform);
        }
    }
}