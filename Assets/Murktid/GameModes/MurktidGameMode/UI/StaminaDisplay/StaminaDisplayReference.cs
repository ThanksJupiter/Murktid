using UnityEngine;

namespace Murktid {

    public class StaminaDisplayReference : MonoBehaviour {
        public MurktidSlider staminaSlider;

        public void UpdateSliderValue(float value) {
            staminaSlider.UpdateSliderValue(value);
        }
    }
}
