using UnityEngine;

namespace Murktid {

    public class AbilityRegenerateStamina : PlayerAbility {

        private float regenTimestamp = float.MinValue;

        protected override void Setup() {
            AddTag(AbilityTags.regenerateStamina);
        }

        public override bool ShouldActivate() {
            if(Context.stamina.stamina.Value >= Context.settings.maxStamina) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(Context.stamina.Value >= Context.settings.maxStamina) {
                return true;
            }

            return false;
        }

        protected override void OnActivate() {
            regenTimestamp = Time.time + Context.settings.defaultStaminaRegenerationDelay;
        }

        protected override void Tick(float deltaTime) {
            if(Time.time >= regenTimestamp) {

                float staminaRegeneration = Context.statusEffectSystem.GetStatusEffectedStaminaRegeneration(Context.settings.defaultStaminaRegenerationRate);
                Context.stamina.RestoreStamina(staminaRegeneration * deltaTime);
            }
        }
    }
}
