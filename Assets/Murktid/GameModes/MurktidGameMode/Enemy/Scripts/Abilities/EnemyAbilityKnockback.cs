using UnityEngine;

namespace Murktid {

    public class EnemyAbilityKnockback : EnemyAbility {

        private float knockbackSpeed = 3f;

        public override bool ShouldActivate() {
            if(!Context.animatorBridge.IsKnockback) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.animatorBridge.IsMovementLocked) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            BlockAbility(AbilityTags.movement, this);
            Context.animatorBridge.AttackReady = false;
            Context.animatorBridge.IsAttacking = false;
            Context.agent.isStopped = true;
            knockbackSpeed = 3f;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
            Context.agent.isStopped = false;
        }

        protected override void Tick(float deltaTime) {

            if(knockbackSpeed > 0f) {
                Context.agent.Move(-Context.transform.forward * (knockbackSpeed * deltaTime));
                knockbackSpeed -= 1.5f * deltaTime;
            }
        }
    }
}
