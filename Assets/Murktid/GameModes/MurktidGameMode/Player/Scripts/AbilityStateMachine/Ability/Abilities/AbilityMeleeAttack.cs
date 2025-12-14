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
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return true;
            }

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

                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.playerEquipmentData.CurrentWeapon.reference.hitbox.isActive = false;
                }

                if(!Context.animatorBridge.IsInMeleeLayer) {
                    didAttack = true;
                }
            }

            if(Context.animatorBridge.IsHitboxActive) {
                hasActivatedHitbox = true;
                Context.animatorBridge.Shoot = false;
                Context.playerEquipmentData.CurrentWeapon.reference.hitbox.isActive = true;

                int overlappedCount = Context.Hitbox.TryGetOverlappedColliders(Context.attackLayerMask, out Collider[] overlappedColliders);
                for(int i = 0; i < overlappedCount; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out ITarget target)) {
                        target.Hit(100f);
                    }
                }
            }
        }
    }
}
