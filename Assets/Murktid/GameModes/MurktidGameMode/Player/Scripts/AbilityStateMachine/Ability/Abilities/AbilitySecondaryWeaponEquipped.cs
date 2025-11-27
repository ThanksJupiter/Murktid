using UnityEngine;

namespace Murktid {

    public class AbilitySecondaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerWeaponData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerWeaponData.currentWeaponType == WeaponType.Secondary) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            // animate the previous one down ._.

            if(Context.playerWeaponData.currentWeapon != null) {
                Context.playerWeaponData.currentWeapon.gameObject.SetActive(false);
            }

            if(Context.playerWeaponData.currentSecondaryWeapon == null) {
                Context.playerWeaponData.currentSecondaryWeapon = Object.Instantiate(
                    Context.playerWeaponData.defaultSecondaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                Context.playerWeaponData.currentWeapon = Context.playerWeaponData.currentSecondaryWeapon;
            }
            else {
                Context.playerWeaponData.currentWeapon = Context.playerWeaponData.currentSecondaryWeapon;
                Context.playerWeaponData.currentWeapon.gameObject.SetActive(true);
            }

            Context.playerWeaponData.currentPrimaryWeapon.transform.localPosition =
                Context.cameraReference.tmpShotgunTransform.localPosition;
            Context.playerWeaponData.currentPrimaryWeapon.transform.localRotation =
                Context.cameraReference.tmpShotgunTransform.localRotation;

            // animate the stuff in
        }
    }
}
