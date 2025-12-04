using UnityEngine;

namespace Murktid {

    public class HealthDisplayReference : MonoBehaviour {
        public MurktidSlider healthSlider;

        public void UpdateSliderValue(float value) {
            healthSlider.UpdateSliderValue(value);
        }
    }
}
