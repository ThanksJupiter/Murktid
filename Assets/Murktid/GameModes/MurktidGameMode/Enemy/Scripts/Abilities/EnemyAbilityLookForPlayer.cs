using UnityEngine;

namespace Murktid {

    public class EnemyAbilityLookForPlayer : EnemyAbility {
        public override bool ShouldActivate() {
            if(Context.hasEngagementSlot) {
                return false;
            }

            if(Context.HasTarget) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.hasEngagementSlot) {
                return true;
            }

            if(Context.HasTarget) {
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

                float distanceToTarget = Vector3.Distance(Context.RayOrigin, collider.transform.position);
                Vector3 directionToPlayer = (collider.transform.position + Vector3.up) - Context.RayOrigin;
                Debug.DrawRay(Context.RayOrigin, directionToPlayer, Color.red);
                bool isPlayerBlocked = Physics.Raycast(Context.RayOrigin, directionToPlayer.normalized, distanceToTarget, Context.obstacleMask);

                if(isPlayerBlocked) {
                    continue;
                }

                Context.controller.SetTarget(player);
                return;
            }
        }
    }
}
