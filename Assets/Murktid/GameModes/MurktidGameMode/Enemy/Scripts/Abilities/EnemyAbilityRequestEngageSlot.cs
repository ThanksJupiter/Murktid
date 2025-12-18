using UnityEngine;

namespace Murktid {

    public class EnemyAbilityRequestEngageSlot : EnemyAbility {
        public override bool ShouldActivate() {
            if(Context.hasEngagementSlot) {
                return false;
            }

            if(!Context.HasTarget) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {

            if(!Context.HasTarget) {
                return true;
            }

            if(Context.hasEngagementSlot) {
                return true;
            }

            return false;
        }

        protected override void Tick(float deltaTime) {

            Vector3 directionToPlayer = (Context.targetPlayer.transform.position + Vector3.up) - Context.RayOrigin;
            bool isPlayerBlocked = Physics.Raycast(Context.RayOrigin, directionToPlayer.normalized, Context.DistanceToTarget, Context.obstacleMask);
            if(!isPlayerBlocked) {
                if(Context.playerSlotSystem.TryClaimEngagementSlot(Context.transform.position, out int claimedIndex)) {
                    Context.hasEngagementSlot = true;
                    Context.engagementSlotIndex = claimedIndex;
                    Context.engagementSlotRequestTimestamp = Time.time + Random.Range(0f, 5f);
                }
            }

            if(Time.time >= Context.engagementSlotRequestTimestamp) {
                Context.engagementSlotRequestTimestamp = Time.time + Context.engagementSlotRequestCooldown;
                // distance
                /*if(Context.DistanceToTarget > Context.stopEngagingDistanceThreshold) {
                    return;
                }*/

                //Debug.Log($"agent remaining distance: {Context.agent.remainingDistance}");

                // line of sight
                if(Physics.Raycast(Context.RayOrigin, directionToPlayer.normalized, Context.DistanceToTarget, Context.obstacleMask)) {
                    Debug.DrawRay(Context.RayOrigin, directionToPlayer, Color.red);
                    return;
                }

                if(Context.agent.remainingDistance <= 0f || float.IsInfinity(Context.agent.remainingDistance)) {
                    return;
                }

                if(Context.playerSlotSystem.TryClaimEngagementSlot(Context.transform.position, out int claimedIndex)) {
                    Context.hasEngagementSlot = true;
                    Context.engagementSlotIndex = claimedIndex;
                    Context.engagementSlotRequestTimestamp = Time.time + Random.Range(0f, 5f);
                }
            }
        }
    }
}
