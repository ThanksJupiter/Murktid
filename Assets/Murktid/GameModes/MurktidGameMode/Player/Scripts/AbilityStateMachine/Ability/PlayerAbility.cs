using KinematicCharacterController;
using UnityEngine;

namespace Murktid {
    public class PlayerAbility : Ability {
        protected PlayerContext Context { get; private set; }

        public override void Setup_Internal(AbilityComponent owningAbilityComponent) {
            base.Setup_Internal(owningAbilityComponent);

            if(owningAbilityComponent is PlayerAbilityComponent playerAbilityComponent) {
                Context = playerAbilityComponent.Context;
            }
        }

        public virtual void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
        }

        public virtual void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
        }

        public virtual void BeforeCharacterUpdate(float deltaTime)
        {
        }

        public virtual void PostGroundingUpdate(float deltaTime)
        {
        }

        public virtual void AfterCharacterUpdate(float deltaTime)
        {
        }

        public virtual bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public virtual void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
        }

        public virtual void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
        }

        public virtual void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
        }

        public virtual void OnDiscreteCollisionDetected(Collider hitCollider)
        {
        }
    }
}
