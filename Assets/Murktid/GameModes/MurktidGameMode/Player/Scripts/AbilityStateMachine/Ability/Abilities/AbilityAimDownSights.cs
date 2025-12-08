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

            if(Context.IsSprinting) {
                return true;
            }

            if(!Context.input.SecondaryAction.IsPressed) {
                return true;
            }

            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.IsAimingDownSights = true;
            Context.animatorBridge.IsADS = true;
            Context.shotgunCrosshair.SetIsADS();
        }

        protected override void OnDeactivate() {
            Context.IsAimingDownSights = false;
            Context.animatorBridge.IsADS = false;
            Context.shotgunCrosshair.SetIsHipfire();
        }
    }
}
