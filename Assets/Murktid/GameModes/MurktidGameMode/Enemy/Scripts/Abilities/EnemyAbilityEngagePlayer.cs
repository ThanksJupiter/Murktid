using UnityEngine;

namespace Murktid {

    public class EnemyAbilityEngagePlayer : EnemyAbility {
        public override bool ShouldActivate() {

            if(Context.IsTargetWithinAttackRange) {
                return false;
            }

            if(Context.hasEngagementSlot) {
                return true;
            }

            return false;
        }

        public override bool ShouldDeactivate() {
            if(Context.IsTargetWithinAttackRange) {
                return true;
            }

            if(!Context.hasEngagementSlot) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsChasing = true;
            Context.agent.speed = Context.settings.defaultChaseSpeed;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsChasing = false;
        }

        protected override void Tick(float deltaTime) {
            Vector3 slotPosition = Context.playerSlotSystem.GetSlotPosition(Context.engagementSlotIndex);
            Debug.DrawLine(Context.transform.position, slotPosition, Color.red);
            Context.agent.SetDestination(slotPosition);

            if(Time.time >= Context.engagementSlotRequestTimestamp) {
                Context.engagementSlotRequestTimestamp = Time.time + Context.engagementSlotRequestCooldown;

                // distance
                if(Context.DistanceToTarget > Context.stopEngagingDistanceThreshold) {
                    Context.playerSlotSystem.ReleaseEngagementSlot(Context.engagementSlotIndex);
                    Context.hasEngagementSlot = false;
                    return;
                }

                Vector3 directionToPlayer = Context.targetPlayer.transform.position - Context.RayOrigin;
                Debug.DrawRay(Context.RayOrigin, directionToPlayer, Color.blue);
                if(Physics.Raycast(Context.RayOrigin, directionToPlayer.normalized, Context.DistanceToTarget, Context.obstacleMask)) {
                    Debug.Log($"Player not in line of sight, releasing engagement slot");
                    Context.playerSlotSystem.ReleaseEngagementSlot(Context.engagementSlotIndex);
                    Context.hasEngagementSlot = false;
                    return;
                }
            }
        }
    }
}
