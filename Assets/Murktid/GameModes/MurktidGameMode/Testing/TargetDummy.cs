using UnityEngine;

namespace Murktid {

    public class TargetDummy : MonoBehaviour, ITarget, IInteractable {

        public void Hit(float damage) {
            //Debug.Log($"Hit: { gameObject.name } for { damage }");
        }

        public void Interact() {
            //Debug.Log($"Interacted with: { gameObject.name }");
        }

        public void Stagger() {

        }
    }
}
