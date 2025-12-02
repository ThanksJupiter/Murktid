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

            if(Context.playerEquipmentData.currentWeapon != null) {
                Context.playerEquipmentData.currentWeapon.gameObject.SetActive(false);
            }

            if(Context.playerEquipmentData.currentPrimaryWeapon == null) {
                Context.playerEquipmentData.currentPrimaryWeapon = Object.Instantiate(
                    Context.playerEquipmentData.defaultPrimaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                Context.playerEquipmentData.currentWeapon = Context.playerEquipmentData.currentPrimaryWeapon;
            }
            else {
                Context.playerEquipmentData.currentWeapon = Context.playerEquipmentData.currentPrimaryWeapon;
                Context.playerEquipmentData.currentWeapon.gameObject.SetActive(true);
            }

            Context.playerEquipmentData.currentPrimaryWeapon.transform.localPosition =
                Context.cameraReference.tmpHammerTransform.localPosition;
            Context.playerEquipmentData.currentPrimaryWeapon.transform.localRotation =
                Context.cameraReference.tmpHammerTransform.localRotation;

            // animate the stuff in
        }
    }
}
