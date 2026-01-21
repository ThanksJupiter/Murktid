using Murktid;
using UnityEngine;

public class TriggerVolume : MonoBehaviour {

    [SerializeField] private FallingObject[] fallingObject;

    private void OnTriggerEnter(Collider other) {

        if(other.TryGetComponent(out PlayerReference playerReference)) {
            foreach (FallingObject fallingObject in fallingObject) {
                fallingObject.Activate();
            }
        }
    }
}
