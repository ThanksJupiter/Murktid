using UnityEngine;

namespace Murktid {

    public class AbilityPrimaryWeaponEquipped : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerEquipmentData.currentWeaponType == WeaponType.Melee) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            // animate the stuff in
        }
    }
}
