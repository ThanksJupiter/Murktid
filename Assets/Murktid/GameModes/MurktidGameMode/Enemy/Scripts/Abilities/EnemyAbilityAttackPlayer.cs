using UnityEngine;

namespace Murktid {

    public class EnemyAbilityAttackPlayer : EnemyAbility {

        private bool hasAttacked = false;
        private bool hasActivatedHitbox = false;
        private float attackRateTimestamp = float.MinValue;
        private float hitboxActivationTimestamp = float.MinValue;

        public override bool ShouldActivate() {
            if(!Context.IsTargetWithinAttackRange) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            return hasAttacked;
        }

        protected override void OnActivate() {
            attackRateTimestamp = Time.time + Context.settings.attackRate;
            hitboxActivationTimestamp = Time.time + 1f;
            Context.animatorBridge.IsAttacking = true;
            hasAttacked = false;
            hasActivatedHitbox = false;
            Context.agent.updateRotation = false;
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.IsAttacking = false;
            Context.agent.updateRotation = true;
        }

        protected override void Tick(float deltaTime) {
            if(Time.time >= attackRateTimestamp) {
                hasAttacked = true;
            }

            Vector3 targetDirection = Context.targetPlayer.transform.position - Context.transform.position;
            targetDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Context.transform.up);
            Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, targetRotation, 1f - Mathf.Exp(-Context.settings.rotateToTargetRate * deltaTime));

            if(hasActivatedHitbox) {
                return;
            }

            if(Time.time >= hitboxActivationTimestamp) {
                hasActivatedHitbox = true;
                Context.animatorBridge.IsAttacking = false;
                Vector3 spherePosition = Context.transform.position + Context.transform.up + Context.transform.forward;
                float sphereSize = .5f;

                Collider[] overlappedColliders = Physics.OverlapSphere(spherePosition, sphereSize, Context.playerMask);
                for(int i = 0; i < overlappedColliders.Length; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out PlayerReference playerReference)) {
                        Debug.Log($"hit player");
                    }
                }
            }
        }
    }
}
