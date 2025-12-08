using R3;
using UnityEngine;

namespace Murktid {

    public class PlayerHealth {

        public ReactiveProperty<float> health;
        public ReactiveProperty<float> maxHealth;

        public ReactiveProperty<float> healthAlpha;

        public PlayerHealth(HealthDisplayReference healthDisplayReference, PlayerSettings playerSettings) {

            health = new ReactiveProperty<float>(playerSettings.maxHealth);
            maxHealth = new ReactiveProperty<float>(playerSettings.maxHealth);
            healthAlpha = new ReactiveProperty<float>(health.Value / maxHealth.Value);

            health.Subscribe(UpdateHealthAlpha);
            healthAlpha.Subscribe(healthDisplayReference.UpdateSliderValue);
        }

        private void UpdateHealthAlpha(float value) {
            healthAlpha.Value = value / maxHealth.Value;
        }

        public void TakeDamage(float damage) {
            health.Value -= damage;
            health.Value = Mathf.Clamp(health.Value, 0, maxHealth.Value);
        }

        public void OnDestroy() {
            health.Dispose();
            maxHealth.Dispose();
            healthAlpha.Dispose();
        }
    }
}
