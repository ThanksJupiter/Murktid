using R3;
using UnityEngine;

namespace Murktid {

    public class EnemyHealth {

        public EnemyHealth(EnemyReference enemyReference) {
            maxHealth = new ReactiveProperty<float>(enemyReference.context.settings.maxHealth);
            currentHealth = new ReactiveProperty<float>(maxHealth.Value);
        }

        public ReactiveProperty<float> maxHealth;
        public ReactiveProperty<float> currentHealth;

        public void TakeDamage(float amount) {
            currentHealth.Value -= amount;
            currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0f, maxHealth.Value);
        }
    }
}
