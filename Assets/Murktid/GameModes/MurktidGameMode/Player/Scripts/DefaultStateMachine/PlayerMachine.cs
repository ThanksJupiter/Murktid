using KinematicCharacterController;
using UnityEngine;
using Utils;

namespace Murktid {
    public class PlayerMachine : StateMachine<PlayerContext>, IMurktidPlayer, ICharacterController {

        public PlayerContext Context {
            get => context;
            private set => context = value;
        }

        public PlayerMachine(PlayerContext context) {
            Context = context;
        }

        public void Initialize() {
            context.motor.CharacterController = this;
            context.cameraReference.PlanarDirection = context.cameraReference.transform.forward;
            context.movementData.activeMoveSpeed = context.settings.defaultMoveSpeed;
            ActivateState<PlayerLocomotionState>();
        }

        public void SetInput() {
            Vector3 moveInputVector = new(context.input.Move.value.x, 0f, context.input.Move.value.y);
            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(context.cameraReference.transform.rotation * Vector3.forward, context.motor.CharacterUp).normalized;

            if(cameraPlanarDirection.sqrMagnitude == 0f) {
                cameraPlanarDirection = Vector3.ProjectOnPlane(context.cameraReference.transform.rotation * Vector3.up, context.motor.CharacterUp).normalized;
            }

            Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, context.motor.CharacterUp);
            context.movementData.moveInputVector = cameraPlanarRotation * moveInputVector;
        }

        public void Tick(float deltaTime) {
            UpdateCameraPositionAndRotation();
        }

        private void UpdateCameraPositionAndRotation() {
            context.cameraReference.TargetVerticalAngle -= (context.input.Look.value.y * context.settings.rotationSpeed);
            context.cameraReference.TargetVerticalAngle = Mathf.Clamp(context.cameraReference.TargetVerticalAngle,
                context.settings.minVerticalLookAngle, context.settings.maxVerticalLookAngle);
            Quaternion verticalRotation = Quaternion.Euler(context.cameraReference.TargetVerticalAngle, 0f, 0f);

            Quaternion rotationFromInput = Quaternion.Euler(context.motor.CharacterUp *
                (context.input.Look.value.x *
                    context.settings.rotationSpeed));
            context.cameraReference.PlanarDirection = rotationFromInput * context.cameraReference.PlanarDirection;
            context.cameraReference.PlanarDirection = Vector3.Cross(context.motor.CharacterUp,
                Vector3.Cross(context.cameraReference.PlanarDirection, context.motor.CharacterUp));
            Quaternion planarRotation =
                Quaternion.LookRotation(context.cameraReference.PlanarDirection, context.motor.CharacterUp);
            Quaternion targetRotation = Quaternion.Slerp(context.cameraReference.transform.rotation, planarRotation * verticalRotation,
                1f - Mathf.Exp(-100f * Time.deltaTime));

            context.cameraReference.transform.rotation = targetRotation;

            context.cameraReference.transform.position = Vector3.Lerp(context.cameraReference.transform.position,
                context.transform.position + Vector3.up * context.settings.cameraHeight,
                1f - Mathf.Exp(-context.settings.cameraFollowSharpness * Time.deltaTime));
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime) {
            if(activeState is IPlayerKinematicControllerState state) {
                state.UpdateRotation(ref currentRotation, deltaTime);
                return;
            }

            Quaternion rotationFromInput = Quaternion.Euler(context.motor.CharacterUp *
                (context.input.Look.value.x *
                    context.settings.rotationSpeed));
            context.cameraReference.PlanarDirection = rotationFromInput * context.cameraReference.PlanarDirection;
            context.cameraReference.PlanarDirection = Vector3.Cross(context.motor.CharacterUp,
                Vector3.Cross(context.cameraReference.PlanarDirection, context.motor.CharacterUp));
            Quaternion planarRotation =
                Quaternion.LookRotation(context.cameraReference.PlanarDirection, context.motor.CharacterUp);
            Quaternion targetRotation =
                Quaternion.Slerp(currentRotation, planarRotation, 1f - Mathf.Exp(-100f * deltaTime));

            currentRotation = targetRotation;
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            if(activeState is IPlayerKinematicControllerState state) {
                state.UpdateVelocity(ref currentVelocity, deltaTime);
                return;
            }

            if(!context.motor.GroundingStatus.IsStableOnGround) {
                currentVelocity += context.settings.gravity * deltaTime;
                return;
            }

            Vector3 inputRight = Vector3.Cross(context.movementData.moveInputVector, context.motor.CharacterUp);
            Vector3 effectiveGroundNormal = context.motor.GroundingStatus.GroundNormal;
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * context.movementData.moveInputVector.magnitude;

            float stableMoveSpeed = context.movementData.activeMoveSpeed; // context.statusEffectSystem.GetStatusEffectedMovementSpeed(context.movement.activeMoveSpeed);

            Vector3 targetMovementVelocity = reorientedInput * stableMoveSpeed;
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-context.settings.movementSharpness * deltaTime));
        }
        public void BeforeCharacterUpdate(float deltaTime) {

        }

        public void PostGroundingUpdate(float deltaTime) {

        }

        public void AfterCharacterUpdate(float deltaTime) {

        }

        public bool IsColliderValidForCollisions(Collider coll) {
            return true;
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) {

        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) {

        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) {

        }

        public void OnDiscreteCollisionDetected(Collider hitCollider) {

        }
    }
}
