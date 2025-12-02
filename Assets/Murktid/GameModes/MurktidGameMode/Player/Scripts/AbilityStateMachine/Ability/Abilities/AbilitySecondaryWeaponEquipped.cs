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

            if(Context.playerEquipmentData.currentWeapon != null) {
                Context.playerEquipmentData.currentWeapon.gameObject.SetActive(false);
            }

            if(Context.playerEquipmentData.currentSecondaryWeapon == null) {
                Context.playerEquipmentData.currentSecondaryWeapon = Object.Instantiate(
                    Context.playerEquipmentData.defaultSecondaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                Context.playerEquipmentData.currentWeapon = Context.playerEquipmentData.currentSecondaryWeapon;
            }
            else {
                Context.playerEquipmentData.currentWeapon = Context.playerEquipmentData.currentSecondaryWeapon;
                Context.playerEquipmentData.currentWeapon.gameObject.SetActive(true);
            }

            if(Context.input.SecondaryAction.IsPressed) {
                Context.shotgunCrosshair.SetIsADS();
            }
            else {
                Context.shotgunCrosshair.Show();
                Context.shotgunCrosshair.SetIsHipfire();
            }

            Context.playerEquipmentData.currentPrimaryWeapon.transform.localPosition =
                Context.cameraReference.tmpShotgunTransform.localPosition;
            Context.playerEquipmentData.currentPrimaryWeapon.transform.localRotation =
                Context.cameraReference.tmpShotgunTransform.localRotation;

            // animate the stuff in
        }

        protected override void OnDeactivate() {
            Context.shotgunCrosshair.Hide();
        }
    }
}
