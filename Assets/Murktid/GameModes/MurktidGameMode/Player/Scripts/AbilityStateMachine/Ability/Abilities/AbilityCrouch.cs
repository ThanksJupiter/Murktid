using UnityEngine;

namespace Murktid {

    public class AbilityCrouch : PlayerAbility {
        public override bool ShouldActivate() {
            if(!Context.input.Crouch.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.input.Crouch.IsPressed) {
                return false;
            }

            if(Context.IsDodgeSliding) {
                return false;
            }

            if(Context.IsSprintSliding) {
                return false;
            }

            // if there are things above blocking uncrouch
            // return false;

            /*if(isCrouching) {
                return false;
            }*/

            return true;
        }

        protected override void OnActivate() {
            //isCrouching = true;
            Context.TargetCapsuleHeight = Context.settings.crouchingCapsuleHeight;
            Context.TargetCameraHeight = Context.settings.crouchingCameraHeight;
            // lower capsule height
            // lower move speed to crouch speed tbh
        }

        protected override void OnDeactivate() {
            Context.TargetCapsuleHeight = Context.settings.standingCapsuleHeight;
            Context.TargetCameraHeight = Context.settings.standingCameraHeight;
        }
    }
}
