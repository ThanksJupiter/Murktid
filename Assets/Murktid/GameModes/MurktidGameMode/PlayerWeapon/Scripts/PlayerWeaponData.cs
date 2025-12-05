using UnityEngine;

namespace Murktid {

    [System.Serializable]
    public class PlayerWeaponData {

        public Transform firePoint;
        public float bulletVelocity = 50f;

        // damage
        public float ADSDamage = 10f;
        public float ADSRange = 25f;
        public float ADSRadius = .5f;

        public float hipfireDamage = 25f;
        public float hipfireRange = 15f;
        public float hipfireRadius = 5f;

        public float recoil = 10f;
        public float fireRate = .25f;

        public int loadedBullets = 2;
        public int maxLoadedBullets = 2;
    }
}
