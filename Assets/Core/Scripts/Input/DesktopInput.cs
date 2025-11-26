using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {

    public class DesktopInput : IApplicationLifecycle, IInput, PlayerInputActions.ICharacterActions {

        private readonly PlayerInputActions inputActions = new();

        public DesktopInput(DesktopInputSettings inputSettings) {
            inputActions.Character.SetCallbacks(this);
            inputActions.Character.Enable();
        }

        public Input<Vector2> Move { get; set; } = new();
        public Input<Vector2> Look { get; set; } = new();
        public Input<bool> PrimaryAction { get; set; } = new();
        public Input<bool> SecondaryAction { get; set; } = new();

        public void Initialize() { }
        public void Tick() { }
        public void LateTick() {
            PrimaryAction.wasPressedThisFrame = false;
            PrimaryAction.wasReleasedThisFrame = false;

            SecondaryAction.wasPressedThisFrame = false;
            SecondaryAction.wasReleasedThisFrame = false;

            Move.wasPressedThisFrame = false;
            Move.wasReleasedThisFrame = false;

            Look.wasPressedThisFrame = false;
            Look.wasReleasedThisFrame = false;
        }
        public void Dispose() { }

        public void OnMove(InputAction.CallbackContext context) {

            if(context.performed) {
                Move.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Move.wasReleasedThisFrame = true;
            }

            Move.value = context.ReadValue<Vector2>();
        }
        public void OnLook(InputAction.CallbackContext context) {

            if(context.performed) {
                Look.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Look.wasReleasedThisFrame = true;
            }

            Look.value = context.ReadValue<Vector2>();
        }
        public void OnPrimaryAction(InputAction.CallbackContext context) {
            if(context.performed) {
                PrimaryAction.value = true;
                PrimaryAction.wasPressedThisFrame = true;
            } else if(context.canceled) {
                PrimaryAction.value = false;
                PrimaryAction.wasReleasedThisFrame = true;
            }
        }
        public void OnSecondaryAction(InputAction.CallbackContext context) {
            if(context.performed) {
                SecondaryAction.value = true;
                SecondaryAction.wasPressedThisFrame = true;
            } else if(context.canceled) {
                SecondaryAction.value = false;
                SecondaryAction.wasReleasedThisFrame = true;
            }
        }
    }
}
