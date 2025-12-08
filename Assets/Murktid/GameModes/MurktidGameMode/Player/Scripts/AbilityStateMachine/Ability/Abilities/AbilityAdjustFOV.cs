using UnityEngine;

namespace Murktid {

    public class AbilityAdjustFOV : PlayerAbility {
        public override bool ShouldActivate() {
            return true;
        }

        public override bool ShouldDeactivate() {
            return false;
        }

        protected override void OnActivate() {
            Context.CurrentFOVTarget = Context.settings.defaultFOV;
        }

        protected override void Tick(float deltaTime) {
            Context.cameraReference.camera.fieldOfView = Mathf.Lerp(Context.cameraReference.camera.fieldOfView, Context.CurrentFOVTarget, 1f - Mathf.Exp(-Context.settings.FOVLerpSpeed * deltaTime));
        }
    }
}
