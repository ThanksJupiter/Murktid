using UnityEngine;

namespace Murktid {

    public class AbstractStatusEffect : ScriptableObject {
        public int sortingOrder = 0;
        public float duration = -1f;
        private float activationTime = 0.0f;

        public string name = "default";
        [TextArea]
        public string description = "";

        public float time => Time.time - activationTime;
        public float remainingTime => duration - time;
        public bool isInfinite => duration <= 0f;

        protected IStatusEffectContext context;

        public void ActivateEffect(IStatusEffectContext context) {
            this.context = context;
            activationTime = Time.time;
        }

        protected void Remove() {
            duration = time;
        }

        public virtual void OnAdded() {}
        public virtual void OnTick(float deltaTime) {}
        public virtual void OnRemoved() {}

        public virtual bool OnEffectRenewed(AbstractStatusEffect statusEffectReference) {
            return false;
        }

        public virtual float GetStatusEffectedStaminaRegeneration(float originalStaminaRegeneration) { return originalStaminaRegeneration; }
        public virtual float GetStatusEffectedRangedDamage(float originalRangedDamage) { return originalRangedDamage; }
        public virtual float GetStatusEffectedMeleeDamage(float originalMeleeDamage) { return originalMeleeDamage; }
        public virtual float GetStatusEffectedDamageTaken(float originalDamageTaken) { return originalDamageTaken; }
        public virtual int GetStatusEffectedBulletPenetration(int originalPenetration) { return originalPenetration; }
    }
}
