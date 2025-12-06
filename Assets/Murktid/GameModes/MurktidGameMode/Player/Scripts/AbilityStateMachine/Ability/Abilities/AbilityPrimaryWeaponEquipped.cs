using UnityEngine;

namespace Murktid {

    public class AbilityPrimaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Primary) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerEquipmentData.currentWeaponType == WeaponType.Primary) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            // animate the previous one down ._.

            if(Context.playerEquipmentData.CurrentWeapon != null) {
                Context.playerEquipmentData.CurrentWeapon.reference.gameObject.SetActive(false);
            }

            if(Context.playerEquipmentData.currentPrimaryWeapon == null) {

                PlayerWeaponReference newWeaponReference = Object.Instantiate(
                    Context.playerEquipmentData.defaultPrimaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                PlayerWeaponData newWeaponData = new(newWeaponReference);
                Context.playerEquipmentData.currentPrimaryWeapon = newWeaponData;
                Context.playerEquipmentData.CurrentWeapon = Context.playerEquipmentData.currentPrimaryWeapon;
            }
            else {
                Context.playerEquipmentData.CurrentWeapon = Context.playerEquipmentData.currentPrimaryWeapon;
                Context.playerEquipmentData.CurrentWeapon.reference.gameObject.SetActive(true);
            }

            Context.playerEquipmentData.currentPrimaryWeapon.reference.transform.localPosition =
                Context.cameraReference.tmpHammerTransform.localPosition;
            Context.playerEquipmentData.currentPrimaryWeapon.reference.transform.localRotation =
                Context.cameraReference.tmpHammerTransform.localRotation;

            // animate the stuff in
        }
    }
}
