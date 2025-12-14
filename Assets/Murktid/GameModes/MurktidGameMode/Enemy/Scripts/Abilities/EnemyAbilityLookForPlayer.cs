using UnityEngine;

namespace Murktid {

    public class EnemyAbilityLookForPlayer : EnemyAbility {
        public override bool ShouldActivate() {
            if(Context.hasEngagementSlot) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.hasEngagementSlot) {
                return true;
            }

            return false;
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

                Context.targetPlayer = player;
                Context.playerSlotSystem = player.context.controller.attackerSlotSystem;
                return;
            }
        }
    }
}
