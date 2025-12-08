using UnityEngine;

namespace Murktid {

    public class AbilitySecondaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            /*if(Context.playerEquipmentData.currentWeaponType != WeaponType.Ranged) {
                return false;
            }*/

            return true;
        }

        public override bool ShouldDeactivate() {

            return false;
            /*if(Context.playerEquipmentData.currentWeaponType == WeaponType.Ranged) {
                return false;
            }

            return true;*/
        }

        protected override void OnActivate() {
            Context.playerEquipmentData.currentWeaponType = WeaponType.Ranged;
            Context.shotgunCrosshair.Show();
        }

        protected override void OnDeactivate() {
            Context.shotgunCrosshair.Hide();
        }
    }
}
