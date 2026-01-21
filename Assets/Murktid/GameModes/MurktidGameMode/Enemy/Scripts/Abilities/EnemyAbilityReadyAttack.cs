using UnityEngine;

namespace Murktid {

    public class EnemyAbilityReadyAttack : EnemyAbility {
        public override bool ShouldActivate() {

            if(!Context.hasAttackSlot) {
                return false;
            }

            if(!Context.HasTarget) {
                return false;
            }

            if(Context.IsTargetWithinAttackRange) {
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

            if(Context.IsTargetWithinAttackRange) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.agent.speed = Context.settings.defaultChaseSpeed;
            Context.animatorBridge.AttackReady = true;
            Context.animatorBridge.IsChasing = true;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsChasing = false;
            Context.agent.isStopped = true;
            Context.agent.ResetPath();
        }

        protected override void Tick(float deltaTime) {
            Context.agent.SetDestination(Context.targetPlayer.transform.position);
        }
    }
}
