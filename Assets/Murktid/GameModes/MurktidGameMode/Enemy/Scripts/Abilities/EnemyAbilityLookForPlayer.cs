using UnityEngine;

namespace Murktid {

    public class EnemyAbilityLookForPlayer : EnemyAbility {
        protected override void Setup() {
            AddTag(AbilityTags.look);
        }

        public override bool ShouldActivate() {
            if(Context.hasTarget) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.hasTarget) {
                return false;
            }

            return true;
        }

        protected override void Tick(float deltaTime) {
            
        }
    }
}
