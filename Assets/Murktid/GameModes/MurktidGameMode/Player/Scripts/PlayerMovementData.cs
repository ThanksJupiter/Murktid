using UnityEngine;

namespace Murktid {

    public class PlayerMovementData {
        public Vector3 moveInputVector;
        public Vector3 lookInputVector;
        public Vector2 inputMoveAxes = Vector2.zero;
        public float activeMoveSpeed;
        public float activeOrientationSharpness;
        public float lastGroundedTimestamp = 0f;
    }
}
