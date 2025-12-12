using UnityEngine;

namespace Murktid {

    public class EnemyAbilityLookForPlayer : EnemyAbility {
        protected override void Setup() {
            AddTag(AbilityTags.look);
        }

        public override bool ShouldActivate() {
            if(Context.HasTarget) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.HasTarget) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsIdle = true;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsIdle = false;
        }

        protected override void Tick(float deltaTime) {
            Collider[] overlappedColliders = Physics.OverlapSphere(Context.transform.position, Context.settings.aggroRange, Context.playerMask);

            for(int i = 0; i < overlappedColliders.Length; i++) {
                Collider collider = overlappedColliders[i];

                if(!collider.TryGetComponent(out PlayerReference player)) {
                    continue;
                }

                // if player has available attacker slot this enemy can occupy
                // begin running toward player
                // if not walk toward player

                Context.targetPlayer = player;
                return;
            }
        }
    }
}
