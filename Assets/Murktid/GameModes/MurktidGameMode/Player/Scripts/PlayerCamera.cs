using UnityEngine;

namespace Murktid {

    public class PlayerCamera : MonoBehaviour {
        public new Camera camera;
        public Vector3 PlanarDirection { get; set; }
        public float TargetVerticalAngle { get; set; }

        [Header("Settings")]
        public float cameraHeight = 1.645f;

        public float rotationSpeed = .25f;
        public float minVerticalAngle = -85f;
        public float maxVerticalAngle = 85f;
        public float followSharpness = 1000f;
    }
}
