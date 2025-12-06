using UnityEngine;

namespace Murktid {

    public class AbilityDodgeSlide : PlayerAbility {
        public override bool ShouldActivate() {
            if(!Context.IsDodging) {
                return false;
            }

            if(!Context.input.Crouch.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            /*if(!Context.input.Crouch.IsPressed) {
                return true;
            }*/

            if(Context.IsDodgeSliding) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {
            BlockAbility(AbilityTags.movement, this);
            Context.IsDodgeSliding = true;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            if(!Context.IsGrounded) {
                return;
            }

            float currentVelocityMagnitude = currentVelocity.magnitude;

            Vector3 effectiveGroundNormal = Context.Motor.GroundingStatus.GroundNormal;

            // Reorient velocity on slope
            currentVelocity = Context.Motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;

            // Calculate target velocity
            Vector3 inputRight = Vector3.Cross(Context.DodgeDirection, Context.Motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * Context.DodgeDirection.magnitude;
            Vector3 targetMovementVelocity = reorientedInput * Context.settings.defaultMoveSpeed;

            // Smooth movement Velocity
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-Context.settings.dodgeSlideMovementSharpness * deltaTime));

            if(currentVelocity.magnitude <= Context.settings.defaultMoveSpeed + 1) {
                Context.IsDodgeSliding = false;
            }
        }
    }
}
