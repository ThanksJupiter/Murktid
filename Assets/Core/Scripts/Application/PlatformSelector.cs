using UnityEngine;

namespace Murktid {

    public class PlatformSelector {
        public readonly DevicePlatform devicePlatform;
        private InputMode inputMode;

        public PlatformSelector(DevicePlatform devicePlatform, InputMode inputMode) {
            this.devicePlatform = devicePlatform;
            this.inputMode = inputMode;
        }

        // why is this new instead of using this.inputMode when setting? for a reason?
        public void SetInputMode(InputMode newInputMode) {
            inputMode = newInputMode;
        }

        public static InputMode GetPlatformDefaultInputMode() {

#if UNITY_ANDROID
            return InputMode.Touch;
#else
            return InputMode.Desktop;
#endif
        }

        public static DevicePlatform GetDevicePlatform() {
#if UNITY_ANDROID
            return DevicePlatform.Touch;
#else
            return DevicePlatform.Desktop;
#endif
        }
    }
}
