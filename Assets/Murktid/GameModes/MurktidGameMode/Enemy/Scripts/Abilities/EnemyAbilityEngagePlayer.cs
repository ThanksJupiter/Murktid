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
        }
    }
}
