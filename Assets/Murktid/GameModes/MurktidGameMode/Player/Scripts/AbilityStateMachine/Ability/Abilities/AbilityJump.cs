using UnityEngine;

namespace Murktid {

    public class AbilityJump : PlayerAbility {
        protected override void Setup() {
            AddTag(AbilityTags.jump);
        }

        public override bool ShouldActivate() {
            if(!Context.settings.allowJumpingWhenSliding && !Context.FoundAnyGround) {
                return false;
            }

            if(!Context.IsGrounded && Context.TimeSinceGrounded > Context.settings.jumpPostGroundingGraceTime) {
                return false;
            }

            if(Context.input.Move.value.y <= 0f) {
                return false;
            }

            float jumpInputTimestamp = Context.input.Jump.pressedTimestamp;
            float timeSinceJumpPressed = Time.time - jumpInputTimestamp;
            bool wantsToJump = timeSinceJumpPressed < Context.settings.jumpPostGroundingGraceTime ||
                    Context.input.Jump.IsPressed;

            return wantsToJump;
        }

        public override bool ShouldDeactivate() {
            if(!Context.IsGrounded) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {
            Context.Motor.ForceUnground();
            Context.motor.BaseVelocity.y = Context.settings.jumpForce;
        }
    }
}
