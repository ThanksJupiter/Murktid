using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "IncreaseStaminaRegenerationEffect", menuName = "Murktid/StatusEffect/IncreaseStaminaRegenerationEffect")]
    public class IncreaseStaminaRegenerationEffect : AbstractStatusEffect {
        public float staminaRegenerationMultiplierIncrease = 1f;

        public override float GetStatusEffectedStaminaRegeneration(float originalStaminaRegeneration) {
            return originalStaminaRegeneration * (1f + staminaRegenerationMultiplierIncrease);
        }
    }
}
