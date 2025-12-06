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
        }

        protected override void OnDeactivate() {
            Context.ActiveMoveSpeed = Context.settings.defaultMoveSpeed;
            Context.IsSprinting = false;
        }
    }
}
