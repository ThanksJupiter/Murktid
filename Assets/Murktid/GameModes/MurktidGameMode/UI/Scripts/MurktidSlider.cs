using UnityEngine;
using UnityEngine.UI;

namespace Murktid {

    public class MurktidSlider : MonoBehaviour {
        public Slider slider;

        public void UpdateSliderValue(float value) {
            slider.value = value;
        }
    }
}
