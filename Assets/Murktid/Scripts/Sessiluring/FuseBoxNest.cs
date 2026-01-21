using UnityEngine;

namespace Murktid {
    public class FuseBoxNest : MonoBehaviour, ITarget {

        [SerializeField] private ActivateLight[] activateLights;

        public void Hit(float damage) {

            if(activateLights != null) {
                foreach(ActivateLight light in activateLights) {
                    if(light != null) {
                        light.Activate();
                    }
                }
            }
            Destroy(gameObject);
        }
        public void Stagger() {

        }

    }

}



