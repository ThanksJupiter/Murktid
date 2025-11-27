using UnityEngine;

namespace Murktid {

    public class AbilityLook : PlayerAbility {
        protected override void Setup() {
            AddTag(AbilityTags.look);
        }

        public override bool ShouldActivate() {
            return true;
        }

        public override bool ShouldDeactivate() {
            return false;
        }

        protected override void OnActivate()
        {
            //CursorHandler.PushState(CursorHandler.CursorState.Locked, this);
            Context.cameraReference.PlanarDirection = Context.cameraReference.transform.forward;
        }

        protected override void OnDeactivate()
        {
            //CursorHandler.ClearInstigator(this);
        }

        protected override void Tick(float deltaTime) {
            Context.cameraReference.TargetVerticalAngle -= (Context.input.Look.value.y * Context.settings.rotationSpeed);
            Context.cameraReference.TargetVerticalAngle = Mathf.Clamp(Context.cameraReference.TargetVerticalAngle,
                Context.settings.minVerticalLookAngle, Context.settings.maxVerticalLookAngle);
            Quaternion verticalRotation = Quaternion.Euler(Context.cameraReference.TargetVerticalAngle, 0f, 0f);

            Quaternion rotationFromInput = Quaternion.Euler(Context.CharacterUp *
                (Context.input.Look.value.x *
                    Context.settings.rotationSpeed));
            Context.cameraReference.PlanarDirection = rotationFromInput * Context.cameraReference.PlanarDirection;
            Context.cameraReference.PlanarDirection = Vector3.Cross(Context.CharacterUp,
                Vector3.Cross(Context.cameraReference.PlanarDirection, Context.CharacterUp));
            Quaternion planarRotation =
                Quaternion.LookRotation(Context.cameraReference.PlanarDirection, Context.CharacterUp);
            Quaternion targetRotation = Quaternion.Slerp(Context.cameraReference.transform.rotation, planarRotation * verticalRotation,
                1f - Mathf.Exp(-100f * deltaTime));

            Context.cameraReference.transform.rotation = targetRotation;
        }

        public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime) {
            Quaternion rotationFromInput = Quaternion.Euler(Context.CharacterUp *
                (Context.input.Look.value.x *
                    Context.settings.rotationSpeed));
            Context.cameraReference.PlanarDirection = rotationFromInput * Context.cameraReference.PlanarDirection;
            Context.cameraReference.PlanarDirection = Vector3.Cross(Context.CharacterUp,
                Vector3.Cross(Context.cameraReference.PlanarDirection, Context.CharacterUp));
            Quaternion planarRotation =
                Quaternion.LookRotation(Context.cameraReference.PlanarDirection, Context.CharacterUp);
            Quaternion targetRotation =
                Quaternion.Slerp(currentRotation, planarRotation, 1f - Mathf.Exp(-100f * deltaTime));

            currentRotation = targetRotation;
        }
    }
}
