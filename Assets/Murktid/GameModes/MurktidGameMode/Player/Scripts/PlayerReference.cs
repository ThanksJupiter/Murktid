using UnityEngine;
using UnityEngine.Serialization;

namespace Murktid {
    public class PlayerReference : MonoBehaviour {
        public PlayerContext context;
        public HealthDisplayReference healthDisplayReference;
        public AmmoDisplayReference ammoDisplayReference;
    }
}
