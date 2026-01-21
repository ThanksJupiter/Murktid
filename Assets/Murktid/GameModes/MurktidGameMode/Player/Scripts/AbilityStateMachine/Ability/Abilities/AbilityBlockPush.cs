using UnityEngine;

namespace Murktid {

    public class AbilityBlockPush : PlayerAbility {

        private bool didAttack = false;
        private bool hasActivatedHitbox = false;
        private bool hadSufficientStamina = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Melee) {
                return false;
            }

            if(!Context.IsBlocking) {
                return false;
            }

            if(!Context.input.PrimaryAction.wasPressedThisFrame) {
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

            BlockAbility(AbilityTags.regenerateStamina, this);

            hadSufficientStamina = Context.stamina.Value > Context.settings.blockPushStaminaCost;
            Context.stamina.ConsumeStamina(Context.settings.blockPushStaminaCost);

            didAttack = false;
            hasActivatedHitbox = false;
            Context.animatorBridge.BlockPush = true;
        }

        protected override void OnDeactivate() {
            UnblockAbilitiesByInstigator(this);
        }

        protected override void Tick(float deltaTime) {

            if(hasActivatedHitbox) {

                if(!Context.animatorBridge.IsHitboxActive) {
                    Context.pushHitbox.isActive = false;
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

                        if(hadSufficientStamina) {
                            target.Stagger();
                        }
                        else {
                            target.Hit(1f);
                        }
                    }
                }
            }
        }
    }
}
