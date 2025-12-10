using UnityEngine;

namespace Murktid {

    public class AbilityInteract : PlayerAbility {

        private bool didInteract = false;

        public override bool ShouldActivate() {
            if(!Context.input.Interact.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {
            return didInteract;
        }

        protected override void OnActivate() {
            didInteract = false;
        }

        protected override void Tick(float deltaTime) {

            float sphereSize = 1f;

            Vector3 spherePosition = Context.transform.position + Vector3.up + Context.transform.forward;
            Collider[] overlappedColliders = Physics.OverlapSphere(spherePosition, sphereSize, Context.interactionLayerMask);
            for(int i = 0; i < overlappedColliders.Length; i++) {
                Collider collider = overlappedColliders[i];
                if(collider.TryGetComponent(out IInteractable interactable)) {
                    interactable.Interact();
                }
            }

            didInteract = true;
        }
    }
}
