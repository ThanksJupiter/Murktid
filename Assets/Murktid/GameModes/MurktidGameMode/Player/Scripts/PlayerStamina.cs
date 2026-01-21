using R3;
using UnityEngine;

namespace Murktid {

    public class PlayerStamina {
        public ReactiveProperty<float> stamina;
        public ReactiveProperty<float> maxStamina;

        public ReactiveProperty<float> staminaAlpha;

        public float Value => stamina.Value;

        public PlayerStamina(StaminaDisplayReference staminaDisplayReference, PlayerSettings playerSettings) {
            stamina = new ReactiveProperty<float>(playerSettings.maxStamina);
            maxStamina = new ReactiveProperty<float>(playerSettings.maxStamina);
            staminaAlpha = new ReactiveProperty<float>(stamina.Value / maxStamina.Value);

            stamina.Subscribe(UpdateStaminaAlpha);
            staminaAlpha.Subscribe(staminaDisplayReference.UpdateSliderValue);
        }

        public void ConsumeStamina(float amount) {
            AlterStamina(-amount);
        }

        public void RestoreStamina(float amount) {
            AlterStamina(amount);
        }

        private void UpdateStaminaAlpha(float value) {
            staminaAlpha.Value = value / maxStamina.Value;
        }

        private void AlterStamina(float amount) {
            stamina.Value += amount;
            stamina.Value = Mathf.Clamp(stamina.Value, 0f, maxStamina.Value);
        }

        public void OnDestroy() {
            stamina.Dispose();
            maxStamina.Dispose();
            staminaAlpha.Dispose();
        }
    }
}
