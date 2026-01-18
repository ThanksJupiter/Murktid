using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class AbilityMeleeAttack : PlayerAbility {

        private bool leaveState = false;
        private bool hasActivatedHitbox = false;
        private bool followUpAttackRequested = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(Context.IsBlocking) {
                return false;
            }

            if(!Context.input.PrimaryAction.IsPressed) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return true;
            }

            if(Context.IsBlocking) {
                return true;
            }

            return leaveState;
        }

        protected override void OnActivate() {
            leaveState = false;
            hasActivatedHitbox = false;
            Context.animatorBridge.Shoot = true;
            Context.animatorBridge.AttackIndex = 0;
            hitboxActivated = false;
        }

        protected override void OnDeactivate() {
            Context.Hitbox.isActive = false;
            Context.animatorBridge.Shoot = false;
            followUpAttackRequested = false;
        }

        private List<ITarget> hitTargets = new List<ITarget>();
        private bool hitboxActivated = false;

        protected override void Tick(float deltaTime) {

            if(Context.animatorBridge.CanQueueFollowUpAttack && Context.input.PrimaryAction.wasPressedThisFrame) {
                followUpAttackRequested = true;
            }

            if(hasActivatedHitbox) {
                if(!Context.animatorBridge.IsHitboxActive) {
                    DeactivateHitbox();

                    if(followUpAttackRequested) {

                        if(Context.animatorBridge.AttackIndex < 1) {
                            Context.animatorBridge.AttackIndex++;
                        }
                        else {
                            Context.animatorBridge.AttackIndex = 0;
                        }

                        Context.animatorBridge.Shoot = true;
                        hasActivatedHitbox = false;
                        followUpAttackRequested = false;
                    }
                }

                if(!Context.animatorBridge.IsInMeleeLayer) {
                    leaveState = true;
                }
            }

            if(Context.animatorBridge.IsHitboxActive) {

                if(!hitboxActivated) {
                    ActivateHitbox();
                }

                TickHitbox();

                hasActivatedHitbox = true;
                Context.animatorBridge.Shoot = false;
            }
        }

        private void ActivateHitbox() {
            hitboxActivated = true;
            hitTargets.Clear();
            Context.Hitbox.isActive = true;
        }

        private void TickHitbox() {
            int overlappedCount = Context.Hitbox.TryGetOverlappedColliders(Context.attackLayerMask, out Collider[] overlappedColliders);
            for(int i = 0; i < overlappedCount; i++) {
                Collider collider = overlappedColliders[i];
                if(collider.TryGetComponent(out ITarget target) && !hitTargets.Contains(target)) {
                    hitTargets.Add(target);
                    target.Hit(75f);

                    Vector3 cameraPosition = Context.cameraReference.transform.position;
                    Vector3 heightAdjustedColliderPosition = new(
                        collider.transform.position.x,
                        Context.cameraReference.transform.position.y,
                        collider.transform.position.z
                        );

                    Vector3 fromHitboxToHitColliderDirection = heightAdjustedColliderPosition - cameraPosition;
                    float halfDistance = fromHitboxToHitColliderDirection.magnitude * .5f;
                    Vector3 hitEffectPosition = cameraPosition + (fromHitboxToHitColliderDirection.normalized * halfDistance);

                    Object.Instantiate(Context.hammerHitEffect, hitEffectPosition, Quaternion.identity);
                }
            }
        }

        private void DeactivateHitbox() {
            Context.Hitbox.isActive = false;
            hitboxActivated = false;
        }
    }
}
