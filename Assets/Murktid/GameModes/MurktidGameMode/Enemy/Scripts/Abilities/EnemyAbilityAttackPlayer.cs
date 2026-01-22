using UnityEngine;

namespace Murktid {

    public class EnemyAbilityAttackPlayer : EnemyAbility {

        private bool hasAttacked = false;
        private bool hasActivatedHitbox = false;

        protected override void Setup() {
            AddTag(AbilityTags.movement);
        }

        public override bool ShouldActivate() {

            if(!Context.hasAttackSlot) {
                return false;
            }

            if(!Context.HasTarget) {
                return false;
            }

            if(!Context.IsTargetWithinAttackRange) {
                return false;
            }

            if(!Context.animatorBridge.AttackReady) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {

            if(Context.animatorBridge.TakeDamage) {
                return true;
            }

            if(Context.animatorBridge.IsKnockback) {
                return true;
            }

            if(!Context.IsTargetWithinAttackRange) {
                return true;
            }

            return hasAttacked;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsAttacking = true;
            hasAttacked = false;
            hasActivatedHitbox = false;
            Context.agent.updateRotation = false;
            //Context.agent.avoidancePriority = 0;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsAttacking = false;
            Context.animatorBridge.AttackReady = false;
            Context.agent.updateRotation = true;
        }

        protected override void Tick(float deltaTime) {

            /*if(Context.IsTargetWithinAttackRange && !Context.animatorBridge.IsAttacking) {
                Context.animatorBridge.IsAttacking = true;
            }*/

            if(Context.animatorBridge.IsAttacking) {
                Vector3 targetDirection = Context.targetPlayer.transform.position - Context.transform.position;
                targetDirection.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Context.transform.up);
                Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, targetRotation, 1f - Mathf.Exp(-Context.settings.rotateToTargetRate * deltaTime));
            }

            if(hasActivatedHitbox) {
                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.hitbox.isActive = false;
                    hasAttacked = true;
                }

                return;
            }

            if(Context.animatorBridge.IsHitboxActive) {
                Context.animatorBridge.AttackReady = false;
                Context.animatorBridge.IsAttacking = false;
                hasActivatedHitbox = true;
                Context.hitbox.isActive = true;
                bool hitPlayer = false;

                int overlappedCount = Context.hitbox.TryGetOverlappedColliders(Context.playerMask, out Collider[] overlappedColliders);
                for(int i = 0; i < overlappedCount; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out PlayerReference playerReference)) {

                        hitPlayer = true;
                        if(playerReference.context.IsBlocking) {

                            bool blockedAttack = playerReference.context.stamina.Value >= playerReference.context.settings.blockAttackStaminaCost;
                            playerReference.context.stamina.ConsumeStamina(playerReference.context.settings.blockAttackStaminaCost);

                            if(blockedAttack) {
                                Context.animatorBridge.IsKnockback = true;
                                playerReference.context.BlockHitIndex = Random.Range(1, 4);
                            }
                            else {
                                playerReference.context.health.TakeDamage(2f);
                            }
                        }
                        else {
                            playerReference.context.health.TakeDamage(5f);
                        }
                    }
                }

                /*if(!hitPlayer) {
                    Context.agent.avoidancePriority = 50;
                }*/
            }
        }
    }
}
