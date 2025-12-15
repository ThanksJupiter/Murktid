using R3;
using UnityEngine;

namespace Murktid {

    public class AbilityMeleeAttack : PlayerAbility {

        private bool didAttack = false;
        private bool hasActivatedHitbox = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(Context.IsBlocking) {
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

            if(Context.IsBlocking) {
                return true;
            }

            return didAttack;
        }

        protected override void OnActivate() {
            didAttack = false;
            hasActivatedHitbox = false;
            Context.animatorBridge.Shoot = true;
        }

        protected override void OnDeactivate() {
            Context.Hitbox.isActive = false;
            Context.animatorBridge.Shoot = false;
        }

        protected override void Tick(float deltaTime) {

            if(hasActivatedHitbox) {

                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.Hitbox.isActive = false;
                }

                if(!Context.animatorBridge.IsInMeleeLayer) {
                    didAttack = true;
                }
            }

            if(Context.animatorBridge.IsHitboxActive) {
                hasActivatedHitbox = true;
                Context.animatorBridge.Shoot = false;
                Context.Hitbox.isActive = true;

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
