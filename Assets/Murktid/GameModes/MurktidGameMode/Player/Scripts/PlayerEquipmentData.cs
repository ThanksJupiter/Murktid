using R3;

namespace Murktid {

    public enum WeaponType {
        Melee,
        Ranged
    }

    [System.Serializable]
    public class PlayerEquipmentData {

        public WeaponType currentWeaponType = WeaponType.Ranged;
        public PlayerWeaponData CurrentWeapon { get => currentWeapon.Value; set => currentWeapon.Value = value; }
        public ReactiveProperty<PlayerWeaponData> currentWeapon = new ReactiveProperty<PlayerWeaponData>();
    }
}
