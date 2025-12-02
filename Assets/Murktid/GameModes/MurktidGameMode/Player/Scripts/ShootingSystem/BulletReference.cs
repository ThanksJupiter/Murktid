using UnityEngine;
using Utils;

namespace Murktid {

    public class BulletReference : MonoBehaviour {
        public BulletData data;
        [Get] public MeshRenderer meshRenderer;
        public ParticleSystem hitParticleSystem;
    }
}
