using UnityEngine;

namespace Murktid {

    public class EnemyAbilityApproachPlayer : EnemyAbility {

        private float range = 15f;
        private float chaseLerpSpeed = .5f;

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
            if(Context.IsTargetWithinRange(range)) {
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

            chaseLerpSpeed = Random.Range(Context.settings.minChaseLerpSpeed, Context.settings.maxChaseLerpSpeed);
            range = Random.Range(Context.settings.minThreatRange, Context.settings.maxThreatRange);
            Context.animatorBridge.IsWalking = true;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsWalking = false;
            Context.agent.isStopped = true;
            Context.agent.ResetPath();
        }

        protected override void Tick(float deltaTime) {

            Context.agent.speed = Mathf.Lerp(Context.agent.speed, Context.settings.maxDefaultWalkSpeed, 1f - Mathf.Exp(-chaseLerpSpeed * deltaTime));
            // this should approach player and stop at a respectable distance

            // add ability to slightly circle / move about when in threat range before given slot by slot system

            Context.agent.SetDestination(Context.targetPlayer.transform.position);
        }
    }
}
