using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {

    [CreateAssetMenu(fileName = "DesktopInputSettings", menuName = "Murktid/Settings/Input/DesktopInputSettings")]
    public class DesktopInputSettings : InputSettings{
        public InputAction movementAction;
        public InputAction lookAction;
        public InputAction primaryAction;
        public InputAction secondaryAction;
    }
}
