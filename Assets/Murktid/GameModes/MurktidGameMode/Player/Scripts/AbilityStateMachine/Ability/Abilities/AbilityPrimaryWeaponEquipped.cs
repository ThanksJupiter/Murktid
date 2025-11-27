using UnityEngine;

namespace Murktid {

    public class AbilityPrimaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerWeaponData.currentWeaponType != WeaponType.Primary) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerWeaponData.currentWeaponType == WeaponType.Primary) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            // animate the previous one down ._.

            if(Context.playerWeaponData.currentWeapon != null) {
                Context.playerWeaponData.currentWeapon.gameObject.SetActive(false);
            }

            if(Context.playerWeaponData.currentPrimaryWeapon == null) {
                Context.playerWeaponData.currentPrimaryWeapon = Object.Instantiate(
                    Context.playerWeaponData.defaultPrimaryWeaponReferencePrefab,
                    Context.cameraReference.weaponHolder);

                Context.playerWeaponData.currentWeapon = Context.playerWeaponData.currentPrimaryWeapon;
            }
            else {
                Context.playerWeaponData.currentWeapon = Context.playerWeaponData.currentPrimaryWeapon;
                Context.playerWeaponData.currentWeapon.gameObject.SetActive(true);
            }

            Context.playerWeaponData.currentPrimaryWeapon.transform.localPosition =
                Context.cameraReference.tmpHammerTransform.localPosition;
            Context.playerWeaponData.currentPrimaryWeapon.transform.localRotation =
                Context.cameraReference.tmpHammerTransform.localRotation;

            // animate the stuff in
        }
    }
}
