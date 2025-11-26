using KinematicCharacterController;
using UnityEngine;

namespace Murktid {
    public class PlayerAbilityComponent : AbilityComponent {
        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.UpdateRotation(ref currentRotation, deltaTime);
                }
            }
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.UpdateVelocity(ref currentVelocity, deltaTime);
                }
            }
        }

        public void BeforeCharacterUpdate(float deltaTime)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.BeforeCharacterUpdate(deltaTime);
                }
            }
        }

        public void PostGroundingUpdate(float deltaTime)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.PostGroundingUpdate(deltaTime);
                }
            }
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.AfterCharacterUpdate(deltaTime);
                }
            }
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.OnGroundHit(hitCollider, hitNormal, hitPoint,
                        ref hitStabilityReport);
                }
            }
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.OnMovementHit(hitCollider, hitNormal, hitPoint,
                        ref hitStabilityReport);
                }
            }
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint,
                        atCharacterPosition, atCharacterRotation, ref hitStabilityReport);
                }
            }
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
            foreach (Ability ability in currentAbilities)
            {
                if (ability is not PlayerAbility playerAbility)
                {
                    continue;
                }

                if (playerAbility.IsActive)
                {
                    playerAbility.OnDiscreteCollisionDetected(hitCollider);
                }
            }
        }
    }
}
