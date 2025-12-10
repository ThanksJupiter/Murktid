using UnityEngine;

namespace Murktid {

    public class AbilityGroundMovement : PlayerAbility {
        protected override void Setup() {
            AddTag(AbilityTags.movement);
        }

        public override bool ShouldActivate() {
            if(!Context.motor.GroundingStatus.IsStableOnGround)
                return false;

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.motor.GroundingStatus.IsStableOnGround)
                return true;

            return false;
        }

        protected override void Tick(float deltaTime) {
            if(Context.input.Move.IsPressed && !Context.animatorBridge.IsWalking) {
                Context.animatorBridge.IsWalking = true;
            }

            if(!Context.input.Move.IsPressed && Context.animatorBridge.IsWalking) {
                Context.animatorBridge.IsWalking = false;
            }

            Vector3 moveInputVector = new(Context.input.Move.value.x, 0f, Context.input.Move.value.y);

            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.forward, Context.CharacterUp).normalized;

            if(cameraPlanarDirection.sqrMagnitude == 0f) {
                cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.up, Context.CharacterUp).normalized;
            }

            Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, Context.CharacterUp);

            Context.MoveInputVector = cameraPlanarRotation * moveInputVector;
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
            Vector3 inputRight = Vector3.Cross(Context.MoveInputVector, Context.Motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * Context.MoveInputVector.magnitude;
            Vector3 targetMovementVelocity = reorientedInput * Context.ActiveMoveSpeed;

            // Smooth movement Velocity
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-Context.settings.movementSharpness * deltaTime));
        }
    }
}
