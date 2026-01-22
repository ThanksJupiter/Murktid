using UnityEngine;

namespace Murktid {

    public class EnemyAbilityReadyAttack : EnemyAbility {

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

            if(Context.animatorBridge.AttackReady) {
                return false;
            }

            if(Context.animatorBridge.IsAttacking) {
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

            if(Context.animatorBridge.IsAttacking) {
                return true;
            }

            /*if(Context.IsTargetWithinAttackRange) {
                return true;
            }*/

            return false;
        }

        protected override void OnActivate() {
            Context.agent.speed = Context.settings.defaultChaseSpeed;
            Context.animatorBridge.AttackReady = true;

            if(!Context.IsTargetWithinAttackRange) {
                Context.animatorBridge.IsChasing = true;
            }
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsChasing = false;
            Context.agent.velocity = Vector3.zero;
            Context.agent.isStopped = true;
            Context.agent.ResetPath();
        }

        protected override void Tick(float deltaTime) {

            if(!Context.IsTargetWithinAttackRange) {
                Context.agent.speed = Context.settings.defaultChaseSpeed;
                Context.agent.SetDestination(Context.targetPlayer.transform.position);
            }
            else {
                Context.animatorBridge.AttackReady = true;
            }
        }
    }
}
