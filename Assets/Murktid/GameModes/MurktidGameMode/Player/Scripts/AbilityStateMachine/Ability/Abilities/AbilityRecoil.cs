using UnityEngine;

namespace Murktid {

    public class AbilityRecoil : PlayerAbility {
        public override bool ShouldActivate() {

            if(Context.recoilOffset <= 0f) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {

            if(Context.recoilOffset > 0f) {
                return false;
            }

            return true;
        }

        protected override void OnActivate() {

        }

        protected override void OnDeactivate() {

        }

        protected override void Tick(float deltaTime) {
            if(Context.recoilOffset > 0f) {
                Context.recoilOffset = Mathf.Lerp(Context.recoilOffset, 0f, 1f - Mathf.Exp(-Context.recoilReturnSpeed * deltaTime));
                //Context.recoilOffset -= Context.recoilReturnSpeed * deltaTime;
            }
        }
    }
}
