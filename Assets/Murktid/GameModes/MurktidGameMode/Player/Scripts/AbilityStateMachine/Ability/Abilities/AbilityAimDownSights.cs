using UnityEngine;

namespace Murktid {

    public class AbilityAimDownSights : PlayerAbility {
        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            if(!Context.input.SecondaryAction.IsPressed) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.input.SecondaryAction.IsPressed) {
                return true;
            }

            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsADS = true;
            Context.shotgunCrosshair.SetIsADS();
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsADS = false;
            Context.shotgunCrosshair.SetIsHipfire();
        }
    }
}
