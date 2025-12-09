using R3;
using UnityEngine;

namespace Murktid {

    public class AbilityMeleeAttack : PlayerAbility {

        private bool didAttack = false;
        private float attackRateTimestamp = float.MinValue;
        private float hitboxActivationTimestamp = float.MinValue;
        private bool hasActivatedHitbox = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(!Context.input.PrimaryAction.IsPressed) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            return didAttack;
        }

        protected override void OnActivate() {
            didAttack = false;
            hasActivatedHitbox = false;
            attackRateTimestamp = Time.time + .45f;
            hitboxActivationTimestamp = Time.time + .15f;
            Context.animatorBridge.Shoot = true;
        }

        protected override void Tick(float deltaTime) {

            if(hasActivatedHitbox) {
                if(!Context.animatorBridge.IsInMeleeLayer) {
                    didAttack = true;
                }

                return;
            }

            if(Context.animatorBridge.IsHitboxActive) {
                hasActivatedHitbox = true;
                Context.animatorBridge.Shoot = false;
                Vector3 spherePosition = Context.playerEquipmentData.CurrentWeapon.reference.firePoint.position;
                Debug.DrawRay(spherePosition, Vector3.up, Color.red, 1f);

                float sphereSize = 1.5f;

                Collider[] overlappedColliders = Physics.OverlapSphere(spherePosition, sphereSize, Context.attackLayerMask);
                for(int i = 0; i < overlappedColliders.Length; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out ITarget target)) {
                        target.Hit(100f);
                    }
                }
            }
        }
    }
}
