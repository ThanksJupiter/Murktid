using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "IncreaseExperienceGainedEffect", menuName = "Murktid/StatusEffect/IncreaseExperienceGainedEffect")]
    public class IncreaseExperienceGainedEffect : AbstractStatusEffect {
        public float baseMultiplierIncrease = .1f;

        private float multiplierIncrease = 1.2f;

        public override bool OnEffectRenewed(AbstractStatusEffect statusEffectReference) {
            multiplierIncrease += baseMultiplierIncrease;

            return true;
        }

        public override float GetStatusEffectedExperienceGained(float originalExperienceGained) {
            return originalExperienceGained * multiplierIncrease;
        }
    }
}
