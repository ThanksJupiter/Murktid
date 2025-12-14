using UnityEngine;

namespace Murktid {

    public class EnemyAbilityRequestAttackSlot : EnemyAbility {
        public override bool ShouldActivate() {

            if(Context.hasAttackSlot) {
                return false;
            }

            if(!Context.IsTargetWithinAttackRange) {
                return false;
            }

            if(!Context.hasEngagementSlot) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.IsTargetWithinAttackRange) {
                return true;
            }

            if(!Context.hasEngagementSlot) {
                return true;
            }

            if(Context.hasAttackSlot) {
                return true;
            }

            return false;
        }

        protected override void Tick(float deltaTime) {

            if(Time.time >= Context.attackSlotRequestTimestamp) {

                Context.attackSlotRequestTimestamp = Time.time + Context.attackSlotRequestCooldown;

                if(Context.playerSlotSystem.TryClaimAttackSlot(Context.engagementSlotIndex)) {
                    Context.attackSlotIndex = Context.engagementSlotIndex;
                    Context.hasAttackSlot = true;
                }
            }
        }
    }
}
