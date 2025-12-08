using R3;
using UnityEngine;

namespace Murktid {

    public class PlayerWeaponSystem : IPlayerSystem {

        private PlayerContext context;
        private AmmoDisplay ammoDisplay;

        public void Initialize(PlayerReference playerReference) {
            context = playerReference.context;
            ammoDisplay = new(playerReference);

            context.playerEquipmentData.currentWeapon.Subscribe(OnEquipNewWeapon);
        }

        public void InstantiateWeapon(PlayerWeaponReference referencePrefab) {

            if(context.playerEquipmentData.CurrentWeapon != null) {
                Object.Destroy(context.playerEquipmentData.CurrentWeapon.reference.gameObject);
            }

            PlayerWeaponReference newWeaponReference = Object.Instantiate(referencePrefab, Vector3.zero, Quaternion.identity, context.cameraReference.transform);
            newWeaponReference.transform.localPosition = Vector3.zero;
            newWeaponReference.transform.localRotation = Quaternion.identity;
            PlayerWeaponData newWeaponData = new(newWeaponReference);
            context.playerEquipmentData.currentWeapon.Value = newWeaponData;
        }

        private void OnEquipNewWeapon(PlayerWeaponData weaponData) {
            context.animatorBridge.SetAnimator(weaponData.reference.animator);
            ammoDisplay.EquipNewWeapon(weaponData);
        }
    }
}
