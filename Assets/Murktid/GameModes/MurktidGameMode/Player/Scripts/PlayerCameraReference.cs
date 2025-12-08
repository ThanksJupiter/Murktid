using UnityEngine;

namespace Murktid {

    public class PlayerCameraReference : MonoBehaviour {
        public new Camera camera;
        public Vector3 PlanarDirection { get; set; }
        public float TargetVerticalAngle { get; set; }
    }
}
