using UnityEngine;

namespace Murktid {

    public class AbilitySecondaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerEquipmentData.currentWeaponType == WeaponType.Secondary) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            // animate the previous one down ._.

            if(Context.playerEquipmentData.CurrentWeapon != null) {
                Context.playerEquipmentData.CurrentWeapon.reference.gameObject.SetActive(false);
            }

            if(Context.playerEquipmentData.currentSecondaryWeapon == null) {

                PlayerWeaponReference newWeaponReference = Object.Instantiate(
                    Context.playerEquipmentData.defaultPrimaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                PlayerWeaponData newWeaponData = new(newWeaponReference);
                Context.playerEquipmentData.currentSecondaryWeapon = newWeaponData;
                Context.playerEquipmentData.CurrentWeapon = Context.playerEquipmentData.currentSecondaryWeapon;
            }
            else {
                Context.playerEquipmentData.CurrentWeapon = Context.playerEquipmentData.currentSecondaryWeapon;
                Context.playerEquipmentData.CurrentWeapon.reference.gameObject.SetActive(true);
            }

            if(Context.input.SecondaryAction.IsPressed) {
                Context.shotgunCrosshair.SetIsADS();
            }
            else {
                Context.shotgunCrosshair.Show();
                Context.shotgunCrosshair.SetIsHipfire();
            }

            Context.playerEquipmentData.currentPrimaryWeapon.reference.transform.localPosition =
                Context.cameraReference.tmpShotgunTransform.localPosition;
            Context.playerEquipmentData.currentPrimaryWeapon.reference.transform.localRotation =
                Context.cameraReference.tmpShotgunTransform.localRotation;

            // animate the stuff in
        }

        protected override void OnDeactivate() {
            Context.shotgunCrosshair.Hide();
        }
    }
}
