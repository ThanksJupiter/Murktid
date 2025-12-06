using UnityEngine;

namespace Murktid {

    public class AbilityCrouch : PlayerAbility {

        //private bool isCrouching = true;
        private float targetCapsuleHeight = 2f;
        private float currentCapsuleHeight = 2f;

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
            targetCapsuleHeight = Context.settings.crouchingCapsuleHeight;
            currentCapsuleHeight = Context.motor.Capsule.height;
            Context.TargetCameraHeight = Context.settings.crouchingCameraHeight;
            // lower capsule height
            // lower move speed to crouch speed tbh
        }

        protected override void OnDeactivate() {
            Context.motor.SetCapsuleDimensions(Context.settings.capsuleRadius, Context.settings.standingCapsuleHeight, Context.settings.standingCapsuleHeight * .5f);
            targetCapsuleHeight = Context.settings.standingCapsuleHeight;
            Context.TargetCameraHeight = Context.settings.standingCameraHeight;
        }

        /*protected override void Tick(float deltaTime) {
            if(!Context.input.Crouch.IsPressed && (targetCapsuleHeight - currentCapsuleHeight) < .1f) {
                isCrouching = false;
                return;
            }
        }*/
    }
}
