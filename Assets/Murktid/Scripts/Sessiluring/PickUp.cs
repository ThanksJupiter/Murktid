using UnityEngine;

namespace Murktid {

    public class PickUp : MonoBehaviour, IInteractable {



        public void Interact() {
            //Debug.Log($"Interacted with: { gameObject.name }");
            Destroy(gameObject);
        }
    }
}
