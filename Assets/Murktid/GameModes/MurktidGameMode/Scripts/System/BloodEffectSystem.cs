using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class BloodEffectSystem {
        private List<ParticleSystem> availableParticleSystems = new List<ParticleSystem>();

        public void PlayBloodSpatterEffect(Vector3 position, Quaternion rotation, GameObject prefab) {
            foreach(ParticleSystem particleSystem in availableParticleSystems) {
                if(!particleSystem.isPlaying) {
                    particleSystem.transform.position = position;
                    particleSystem.transform.rotation = rotation;
                    particleSystem.Play();
                    return;
                }
            }

            ParticleSystem newParticleSystem = Object.Instantiate(prefab).GetComponent<ParticleSystem>();
            newParticleSystem.transform.position = position;
            newParticleSystem.transform.rotation = rotation;
            availableParticleSystems.Add(newParticleSystem);
            newParticleSystem.Play();
        }
    }
}
