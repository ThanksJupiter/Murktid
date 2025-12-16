using UnityEngine;

namespace Murktid {

    public class AbilityBlockPush : PlayerAbility {

        private bool didAttack = false;
        private bool hasActivatedHitbox = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(!Context.IsBlocking) {
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
            Context.animatorBridge.BlockPush = true;
        }

        protected override void Tick(float deltaTime) {

            if(hasActivatedHitbox) {

                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.pushHitbox.isActive = false;
                }

                if(!Context.animatorBridge.IsInMeleeLayer) {
                    didAttack = true;
                }
            }

            if(Context.animatorBridge.IsHitboxActive) {
                hasActivatedHitbox = true;
                Context.animatorBridge.BlockPush = false;
                Context.pushHitbox.isActive = true;

                int overlappedCount = Context.pushHitbox.TryGetOverlappedColliders(Context.attackLayerMask, out Collider[] overlappedColliders);
                for(int i = 0; i < overlappedCount; i++) {
                    Collider collider = overlappedColliders[i];
                    if(collider.TryGetComponent(out ITarget target)) {
                        target.Hit(10f);
                    }
                }
            }
        }
    }
}
