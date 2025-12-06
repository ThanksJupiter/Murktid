using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Murktid {

    public enum WeaponType {
        Primary,
        Secondary
    }

    [System.Serializable]
    public class PlayerEquipmentData {

        public WeaponType currentWeaponType = WeaponType.Primary;
        public PlayerWeaponData CurrentWeapon { get => currentWeapon.Value; set => currentWeapon.Value = value; }
        public ReactiveProperty<PlayerWeaponData> currentWeapon = new ReactiveProperty<PlayerWeaponData>();

        [HideInInspector]
        public PlayerWeaponData currentPrimaryWeapon;
        [HideInInspector]
        public PlayerWeaponData currentSecondaryWeapon;

        [Header("Defaults")]
        public PlayerWeaponReference defaultPrimaryWeaponReferencePrefab;
        public PlayerWeaponReference defaultSecondaryWeaponReferencePrefab;
    }
}
