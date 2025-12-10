using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Murktid {

    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Murktid/Weapon/Config")]
    public class PlayerWeaponConfig : ScriptableObject {

        public float bulletVelocity = 50f;
        public float bulletVelocityRandomize = 10f;

        public float shotgunBarrelSeparationMultiplier = .2f;

        // damage
        public float ADSDamage = 10f;
        public float ADSRange = 25f;
        public float ADSRadius = .5f;

        public float hipfireDamage = 25f;
        public float hipfireRange = 15f;
        public float hipfireRadius = 5f;

        public float recoil = 10f;
        public float fireRate = .25f;

        public int maxLoadedBullets = 2;
        public int maxBulletsInReserve = 80;
    }
}
