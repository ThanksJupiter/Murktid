using UnityEngine;

namespace Murktid {

    public class EnemyAbilityChasePlayer : EnemyAbility {
        public override bool ShouldActivate() {
            if(!Context.HasTarget) {
                return false;
            }

            if(Context.IsTargetWithinAttackRange) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.IsTargetWithinAttackRange) {
                return true;
            }

            if(!Context.HasTarget) {
                return true;
            }

            // if is dead?

            return false;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsChasing = true;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsChasing = false;
        }

        protected override void Tick(float deltaTime) {
            Context.agent.SetDestination(Context.targetPlayer.transform.position);
        }
    }
}
