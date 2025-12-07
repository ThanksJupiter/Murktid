using UnityEngine;

namespace Murktid {

    public class AbilitySprint : PlayerAbility {
        public override bool ShouldActivate() {

            if(!Context.IsGrounded) {
                return false;
            }

            if(!Context.input.Sprint.value) {
                return false;
            }

            if(Context.input.Move.value.y <= 0f) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            if(!Context.IsGrounded) {
                return true;
            }

            if(Context.input.Sprint.IsPressed) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {
            Context.ActiveMoveSpeed = Context.settings.sprintMoveSpeed;
            Context.IsSprinting = true;
            Context.CurrentFOVTarget = Context.settings.sprintFOV;
        }

        protected override void OnDeactivate() {
            Context.ActiveMoveSpeed = Context.settings.defaultMoveSpeed;
            Context.IsSprinting = false;
            Context.CurrentFOVTarget = Context.settings.defaultFOV;
        }
    }
}
