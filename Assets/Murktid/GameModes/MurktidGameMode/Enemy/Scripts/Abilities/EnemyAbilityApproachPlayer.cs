using UnityEngine;

namespace Murktid {

    public class EnemyAbilityApproachPlayer : EnemyAbility {

        protected override void Setup() {
            AddTag(AbilityTags.movement);
        }

        public override bool ShouldActivate() {

            if(Context.hasEngagementSlot) {
                return false;
            }

            if(!Context.HasTarget) {
                return false;
            }

            if(Context.IsTargetWithinThreatRange) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.IsTargetWithinThreatRange) {
                return true;
            }

            if(Context.hasEngagementSlot) {
                return true;
            }

            if(!Context.HasTarget) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            Context.animatorBridge.IsWalking = true;
            Context.agent.speed = Context.settings.defaultWalkSpeed;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsWalking = false;
            Context.agent.isStopped = true;
            Context.agent.ResetPath();
        }

        protected override void Tick(float deltaTime) {
            Context.agent.SetDestination(Context.targetPlayer.transform.position);
        }
    }
}
