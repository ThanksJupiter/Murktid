using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "IncreaseDamageStatusEffect", menuName = "Murktid/StatusEffect/IncreaseDamageStatusEffect")]
    public class IncreaseDamageStatusEffect : AbstractStatusEffect {

        public float baseDamageIncrease = 5f;

        private float damageIncrease = 5f;

        public override bool OnEffectRenewed(AbstractStatusEffect statusEffectReference) {
            damageIncrease += baseDamageIncrease;
            return true;
        }

        public override float GetStatusEffectedMeleeDamage(float originalMeleeDamage) {
            return originalMeleeDamage + damageIncrease;
        }

        public override float GetStatusEffectedRangedDamage(float originalRangedDamage) {
            return originalRangedDamage + damageIncrease;
        }
    }
}
