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

            if(Time.time >= Context.engagementSlotRequestTimestamp) {
                Context.engagementSlotRequestTimestamp = Time.time + Context.engagementSlotRequestCooldown;

                if(Context.playerSlotSystem.TryClaimEngagementSlot(Context.transform.position, out int claimedIndex)) {
                    Context.hasEngagementSlot = true;
                    Context.engagementSlotIndex = claimedIndex;
                }
            }
        }
    }
}
