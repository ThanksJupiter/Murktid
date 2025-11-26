using UnityEngine;

namespace Murktid {

    public class PlayerCamera : MonoBehaviour {
        public new Camera camera;
        public Vector3 PlanarDirection { get; set; }
        public float TargetVerticalAngle { get; set; }
    }
}
