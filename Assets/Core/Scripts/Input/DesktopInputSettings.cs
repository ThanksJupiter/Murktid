using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {

    public enum InputType {
        Default,
        Cool
    }

    [CreateAssetMenu(fileName = "DesktopInputSettings", menuName = "Murktid/Settings/Input/DesktopInputSettings")]
    public class DesktopInputSettings : InputSettings {

        public InputType inputType = InputType.Default;

        public InputAction movementAction;
        public InputAction lookAction;
        public InputAction primaryAction;
        public InputAction secondaryAction;
    }
}
