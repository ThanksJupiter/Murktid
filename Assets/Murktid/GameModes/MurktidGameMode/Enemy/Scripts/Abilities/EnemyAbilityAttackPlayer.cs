using UnityEngine;

namespace Murktid {

    public class EnemyAbilityAttackPlayer : EnemyAbility {

        private bool hasAttacked = false;
        private bool hasActivatedHitbox = false;

        public override bool ShouldActivate() {

            if(!Context.hasAttackSlot) {
                return false;
            }

            if(!Context.HasTarget) {
                return false;
            }

            if(!Context.hasEngagementSlot) {
                return false;
            }

            if(!Context.IsTargetWithinAttackRange) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            return hasAttacked;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsAttacking = true;
            hasAttacked = false;
            hasActivatedHitbox = false;
            Context.agent.updateRotation = false;
            Context.agent.avoidancePriority = 0;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsAttacking = false;
            Context.agent.updateRotation = true;
            Context.playerSlotSystem.ReleaseAttackSlot(Context.attackSlotIndex);
            Context.hasAttackSlot = false;
        }

        protected override void Tick(float deltaTime) {
            Vector3 targetDirection = Context.targetPlayer.transform.position - Context.transform.position;
            targetDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Context.transform.up);
            Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, targetRotation, 1f - Mathf.Exp(-Context.settings.rotateToTargetRate * deltaTime));

            if(hasActivatedHitbox) {
                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.hitbox.isActive = false;
                    hasAttacked = true;
                }

                return;
            }

            if(Context.animatorBridge.IsHitboxActive) {
                hasActivatedHitbox = true;
                Context.hitbox.isActive = true;
                bool hitPlayer = false;

                int overlappedCount = Context.hitbox.TryGetOverlappedColliders(Context.playerMask, out Collider[] overlappedColliders);
                for(int i = 0; i < overlappedCount; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out PlayerReference playerReference)) {

                        hitPlayer = true;
                        if(playerReference.context.IsBlocking) {
                            playerReference.context.BlockHitIndex = Random.Range(1, 4);
                        }
                        else {
                            playerReference.context.health.TakeDamage(10f);
                        }
                    }
                }

                if(!hitPlayer) {
                    Context.playerSlotSystem.ReleaseEngagementSlot(Context.engagementSlotIndex);
                    Context.hasEngagementSlot = false;
                    Context.agent.avoidancePriority = 50;
                }
            }
        }
    }
}
