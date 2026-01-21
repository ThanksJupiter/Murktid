using UnityEngine;

namespace Murktid {

    public class AbilitySprintSlide : PlayerAbility {

        private bool setVelocity = true;
        private bool hadSufficientStamina = false;

        public override bool ShouldActivate() {
            if(!Context.IsSprinting) {
                return false;
            }

            if(!Context.input.Crouch.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.IsSprintSliding) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

            hadSufficientStamina = Context.stamina.Value > Context.settings.sprintSlideStaminaCost;

            setVelocity = true;
            BlockAbility(AbilityTags.movement, this);
            BlockAbility(AbilityTags.regenerateStamina, this);
            Context.IsSprintSliding = true;
            Context.DodgeDirection = Context.motor.Velocity.normalized;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            if(!Context.IsGrounded) {
                return;
            }

            if(setVelocity) {
                currentVelocity = Context.DodgeDirection * Context.settings.sprintSlideSpeed;

                if(!hadSufficientStamina) {
                    currentVelocity = Context.DodgeDirection * (Context.settings.sprintSlideSpeed * .75f);
                }

                setVelocity = false;
                return;
            }

            float currentVelocityMagnitude = currentVelocity.magnitude;

            Vector3 effectiveGroundNormal = Context.Motor.GroundingStatus.GroundNormal;

            // Reorient velocity on slope
            currentVelocity = Context.Motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;

            // Calculate target velocity
            Vector3 inputRight = Vector3.Cross(Context.DodgeDirection, Context.Motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * Context.DodgeDirection.magnitude;
            Vector3 targetMovementVelocity = reorientedInput * Context.settings.sprintMoveSpeed;

            // Smooth movement Velocity
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-Context.settings.sprintSlideMovementSharpness * deltaTime));

            if(currentVelocity.magnitude <= Context.settings.sprintMoveSpeed + 1) {
                Context.IsSprintSliding = false;
            }
        }
    }
}
