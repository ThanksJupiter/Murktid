using KinematicCharacterController;
using UnityEngine;

namespace Murktid {

    public class PlayerMovementController : ICharacterController {

        private PlayerContext context;
        private PlayerAbilityComponent abilityComponent;

        public void Initialize(PlayerContext context, PlayerAbilityComponent abilityComponent) {
            this.context = context;
            this.abilityComponent = abilityComponent;
            context.motor.CharacterController = this;
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime) {
            abilityComponent.UpdateRotation(ref currentRotation, deltaTime);
        }
        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            abilityComponent.UpdateVelocity(ref currentVelocity, deltaTime);
        }
        public void BeforeCharacterUpdate(float deltaTime) {
            abilityComponent.BeforeCharacterUpdate(deltaTime);
        }
        public void PostGroundingUpdate(float deltaTime) {
            abilityComponent.PostGroundingUpdate(deltaTime);
        }
        public void AfterCharacterUpdate(float deltaTime) {
            abilityComponent.AfterCharacterUpdate(deltaTime);
        }
        public bool IsColliderValidForCollisions(Collider coll) {
            return true;
        }
        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) {
            abilityComponent.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);
        }
        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) {
            abilityComponent.OnMovementHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);
        }
        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) {
            abilityComponent.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint, atCharacterPosition, atCharacterRotation, ref hitStabilityReport);
        }
        public void OnDiscreteCollisionDetected(Collider hitCollider) {
            abilityComponent.OnDiscreteCollisionDetected(hitCollider);
        }
    }
}
