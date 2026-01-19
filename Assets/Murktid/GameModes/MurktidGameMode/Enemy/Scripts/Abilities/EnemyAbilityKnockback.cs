using UnityEngine;

namespace Murktid {

    public class EnemyAbilityKnockback : EnemyAbility {
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
            Context.agent.isStopped = true;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
            Context.agent.isStopped = false;
        }
    }
}
