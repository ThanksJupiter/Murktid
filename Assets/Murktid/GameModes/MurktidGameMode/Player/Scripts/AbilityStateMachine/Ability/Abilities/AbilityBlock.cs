using UnityEngine;

namespace Murktid {

    public class AbilityBlock : PlayerAbility {

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(!Context.input.SecondaryAction.IsPressed) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return true;
            }

            if(!Context.input.SecondaryAction.IsPressed) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.IsBlocking = true;
            Context.animatorBridge.IsBlocking = true;
        }

        protected override void OnDeactivate() {
            Context.IsBlocking = false;
            Context.animatorBridge.IsBlocking = false;
            Context.BlockHitIndex = 0;
            Context.animatorBridge.BlockHitIndex = 0;
        }

        protected override void Tick(float deltaTime) {

            if(Context.BlockHitIndex != 0) {
                Context.animatorBridge.BlockHitIndex = Context.BlockHitIndex;
                Context.BlockHitIndex = 0;
            }

            if(Context.animatorBridge.IsHitboxActive) {
                Context.animatorBridge.BlockHitIndex = 0;
            }
        }
    }
}
