using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {

    public class DesktopInput : IApplicationLifecycle, IInput, PlayerInputActions.ICharacterActions, PlayerInputActions.ICoolCharacterActions {

        private readonly PlayerInputActions inputActions = new();

        public DesktopInput(DesktopInputSettings inputSettings) {

            switch(inputSettings.inputType) {

                case InputType.Default:
                    inputActions.Character.SetCallbacks(this);
                    inputActions.Character.Enable();
                    break;
                case InputType.Cool:
                    inputActions.CoolCharacter.SetCallbacks(this);
                    inputActions.CoolCharacter.Enable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Input<Vector2> Move { get; set; } = new();
        public Input<Vector2> Look { get; set; } = new();
        public Input<bool> PrimaryAction { get; set; } = new();
        public Input<bool> SecondaryAction { get; set; } = new();
        public Input<bool> Dodge { get; set; } = new();
        public Input<bool> Jump { get; set; } = new();
        public Input<bool> Crouch { get; set; } = new();
        public Input<bool> Sprint { get; set; } = new();
        public Input<bool> EquipPrimaryWeapon { get; set; } = new();
        public Input<bool> EquipSecondaryWeapon { get; set; } = new();
        public Input<bool> SwitchWeapon { get; set; } = new();
        public Input<bool> Reload { get; set; } = new();
        public Input<bool> Interact { get; set; } = new();

        public void Initialize() { }
        public void Tick() { }
        public void LateTick() {
            Move.wasPressedThisFrame = false;
            Move.wasReleasedThisFrame = false;

            Look.wasPressedThisFrame = false;
            Look.wasReleasedThisFrame = false;

            PrimaryAction.wasPressedThisFrame = false;
            PrimaryAction.wasReleasedThisFrame = false;

            SecondaryAction.wasPressedThisFrame = false;
            SecondaryAction.wasReleasedThisFrame = false;

            Dodge.wasPressedThisFrame = false;
            Dodge.wasReleasedThisFrame = false;

            Jump.wasPressedThisFrame = false;
            Jump.wasReleasedThisFrame = false;

            Crouch.wasPressedThisFrame = false;
            Crouch.wasReleasedThisFrame = false;

            Sprint.wasPressedThisFrame = false;
            Sprint.wasReleasedThisFrame = false;

            EquipPrimaryWeapon.wasPressedThisFrame = false;
            EquipPrimaryWeapon.wasReleasedThisFrame = false;

            EquipSecondaryWeapon.wasPressedThisFrame = false;
            EquipSecondaryWeapon.wasReleasedThisFrame = false;

            SwitchWeapon.wasPressedThisFrame = false;
            SwitchWeapon.wasReleasedThisFrame = false;

            Reload.wasPressedThisFrame = false;
            Reload.wasReleasedThisFrame = false;

            Interact.wasPressedThisFrame = false;
            Interact.wasReleasedThisFrame = false;
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
        public void OnEquipPrimaryWeapon(InputAction.CallbackContext context) {
            if(context.performed) {
                EquipPrimaryWeapon.value = true;
                EquipPrimaryWeapon.wasPressedThisFrame = true;
            } else if(context.canceled) {
                EquipPrimaryWeapon.value = false;
                EquipPrimaryWeapon.wasReleasedThisFrame = true;
            }
        }
        public void OnEquipSecondaryWeapon(InputAction.CallbackContext context) {
            if(context.performed) {
                EquipSecondaryWeapon.value = true;
                EquipSecondaryWeapon.wasPressedThisFrame = true;
            } else if(context.canceled) {
                EquipSecondaryWeapon.value = false;
                EquipSecondaryWeapon.wasReleasedThisFrame = true;
            }
        }
        public void OnSwitchWeapon(InputAction.CallbackContext context) {
            if(context.performed) {
                SwitchWeapon.value = true;
                SwitchWeapon.wasPressedThisFrame = true;
            } else if(context.canceled) {
                SwitchWeapon.value = false;
                SwitchWeapon.wasReleasedThisFrame = true;
            }
        }
        public void OnSprint(InputAction.CallbackContext context) {
            if(context.performed) {
                Sprint.value = true;
                Sprint.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Sprint.value = false;
                Sprint.wasReleasedThisFrame = true;
            }
        }
        public void OnDodge(InputAction.CallbackContext context) {
            if(context.performed) {
                Dodge.pressedTimestamp = Time.time;
                Dodge.value = true;
                Dodge.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Dodge.value = false;
                Dodge.wasReleasedThisFrame = true;
            }
        }
        public void OnJump(InputAction.CallbackContext context) {
            if(context.performed) {
                Jump.pressedTimestamp = Time.time;
                Jump.value = true;
                Jump.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Jump.value = false;
                Jump.wasReleasedThisFrame = true;
            }
        }
        public void OnCrouch(InputAction.CallbackContext context) {
            if(context.performed) {
                Crouch.pressedTimestamp = Time.time;
                Crouch.value = true;
                Crouch.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Crouch.value = false;
                Crouch.wasReleasedThisFrame = true;
            }
        }
        public void OnReload(InputAction.CallbackContext context) {
            if(context.performed) {
                Reload.pressedTimestamp = Time.time;
                Reload.value = true;
                Reload.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Reload.value = false;
                Reload.wasReleasedThisFrame = true;
            }
        }

        public void OnInteract(InputAction.CallbackContext context) {
            if(context.performed) {
                Interact.pressedTimestamp = Time.time;
                Interact.value = true;
                Interact.wasPressedThisFrame = true;
            } else if(context.canceled) {
                Interact.value = false;
                Interact.wasReleasedThisFrame = true;
            }
        }
    }
}
