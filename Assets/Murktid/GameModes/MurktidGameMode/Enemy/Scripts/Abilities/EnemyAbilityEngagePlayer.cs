using UnityEngine;
using UnityEngine.AI;

namespace Murktid {

    public class EnemyAbilityEngagePlayer : EnemyAbility {

        private int strikes = 0;

        protected override void Setup() {
            AddTag(AbilityTags.movement);
        }

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
            Context.agent.avoidancePriority = 0;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsChasing = false;
            Context.agent.avoidancePriority = 50;
        }

        protected override void Tick(float deltaTime) {
            Vector3 slotPosition = Context.playerSlotSystem.GetSlotPosition(Context.engagementSlotIndex);
            Debug.DrawLine(Context.transform.position, slotPosition, Color.red);
            Context.agent.SetDestination(slotPosition);

            if(Time.time >= Context.engagementSlotRequestTimestamp) {
                Context.engagementSlotRequestTimestamp = Time.time + Context.engagementSlotRequestCooldown;

                Vector3 directionToPlayer = (Context.targetPlayer.transform.position + Vector3.up) - Context.RayOrigin;
                bool isPlayerBlocked = Physics.Raycast(Context.RayOrigin, directionToPlayer.normalized, Context.DistanceToTarget, Context.obstacleMask);
                Debug.DrawRay(Context.RayOrigin, directionToPlayer, Color.blue);

                bool lostEngagement = false;
                //Debug.Log($"agent remaining distance: {Context.agent.remainingDistance}");

                // want to stop engaging player if
                // agent.remainingdistance is absurd

                string message = $"Stop engage because: ";

                // distance
                if(Context.DistanceToTarget > Context.stopEngagingDistanceThreshold && isPlayerBlocked) {
                    lostEngagement = true;
                    message += $"distance too great and no line of sight";
                }
                else if(isPlayerBlocked && Context.agent.remainingDistance > Context.stopEngagingDistanceThreshold) {
                    //Debug.Log($"Player not in line of sight, releasing engagement slot");
                    lostEngagement = true;
                    message += $"agent remaining distance: {Context.agent.remainingDistance} & no line of sight";
                }

                if(lostEngagement || strikes >= 2) {
                    strikes = 0;
                    Context.playerSlotSystem.ReleaseEngagementSlot(Context.engagementSlotIndex);
                    Context.hasEngagementSlot = false;
                    //Debug.Log(message);
                }
                else {
                    strikes++;
                }
            }
        }
    }
}
