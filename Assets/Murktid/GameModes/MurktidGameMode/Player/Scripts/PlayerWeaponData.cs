using UnityEngine;
using UnityEngine.Serialization;

namespace Murktid {

    public enum WeaponType {
        Primary,
        Secondary
    }

    [System.Serializable]
    public class PlayerWeaponData {
        public WeaponType currentWeaponType = WeaponType.Primary;

        [HideInInspector]
        public PlayerWeaponReference currentPrimaryWeapon;
        [HideInInspector]
        public PlayerWeaponReference currentSecondaryWeapon;

        public PlayerWeaponReference currentWeapon = null;

        [Header("Defaults")]
        public PlayerWeaponReference defaultPrimaryWeaponReferencePrefab;
        public PlayerWeaponReference defaultSecondaryWeaponReferencePrefab;
    }
}
