using UnityEngine;

namespace Murktid {

    public class AbilityDodge : PlayerAbility {

        private bool setVelocity = true;
        private bool hasDodged = false;
        private float dodgeCompleteTimestamp = float.MinValue;
        private bool hadSufficientStamina = false;

        public override bool ShouldActivate() {
            if(!Context.input.Dodge.wasPressedThisFrame) {
                return false;
            }

            if(Context.input.Move.value.y > 0f && Context.input.Move.value.x == 0f) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            return hasDodged;
        }

        protected override void OnActivate() {

            hadSufficientStamina = Context.stamina.Value > Context.settings.dodgeStaminaCost;
            Context.stamina.ConsumeStamina(Context.settings.dodgeStaminaCost);

            setVelocity = true;
            BlockAbility(AbilityTags.movement, this);
            BlockAbility(AbilityTags.regenerateStamina, this);
            hasDodged = false;
            dodgeCompleteTimestamp = Time.time + Context.settings.dodgeDuration;

            if(!hadSufficientStamina) {
                dodgeCompleteTimestamp = Time.time + (Context.settings.dodgeDuration * .25f);
            }

            Context.IsDodging = true;

            Vector3 moveInputVector = new(Context.input.Move.value.x, 0f, Context.input.Move.value.y);

            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.forward, Context.CharacterUp).normalized;

            if (cameraPlanarDirection.sqrMagnitude == 0f)
            {
                cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.up, Context.CharacterUp).normalized;
            }

            Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, Context.CharacterUp);

            Context.DodgeDirection = cameraPlanarRotation * moveInputVector;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
            Context.IsDodging = false;
        }

        protected override void Tick(float deltaTime) {
            if(Time.time >= dodgeCompleteTimestamp) {
                hasDodged = true;
            }
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            if (!Context.IsGrounded)
            {
                return;
            }

            if(setVelocity) {
                currentVelocity = Context.DodgeDirection * Context.settings.sprintSlideSpeed;

                if(!hadSufficientStamina) {
                    currentVelocity = Context.DodgeDirection * (Context.settings.sprintSlideSpeed * .5f);
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
            Vector3 targetMovementVelocity = reorientedInput * Context.settings.dodgeSpeed;

            // Smooth movement Velocity
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-Context.settings.movementSharpness * deltaTime));
        }
    }
}
