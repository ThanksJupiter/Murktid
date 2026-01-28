using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "IncreaseBulletPenetration", menuName = "Murktid/StatusEffect/IncreaseBulletPenetration")]
    public class IncreaseBulletPenetration : AbstractStatusEffect {
        public int baseMultiplierIncrease = 1;

        private int multiplierIncrease = 2;

        public override bool OnEffectRenewed(AbstractStatusEffect statusEffectReference) {
            multiplierIncrease += baseMultiplierIncrease;
            return true;
        }

        public override int GetStatusEffectedBulletPenetration(int originalPenetration) {
            return originalPenetration * multiplierIncrease;
        }
    }
}
