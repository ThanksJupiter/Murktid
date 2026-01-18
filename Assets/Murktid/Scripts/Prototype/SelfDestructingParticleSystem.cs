using System;
using UnityEngine;

namespace Murktid {

    public class SelfDestructingParticleSystem : MonoBehaviour {

        public new ParticleSystem particleSystem;

        private void Update() {
            if(!particleSystem.isPlaying) {
                Destroy(gameObject);
            }
        }
    }
}
