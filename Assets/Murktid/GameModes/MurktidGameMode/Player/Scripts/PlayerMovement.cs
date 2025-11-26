using UnityEngine;

namespace Murktid {

    public class PlayerMovement {
        public Vector3 moveInputVector;
        public Vector3 lookInputVector;
        public Vector2 inputMoveAxes = Vector2.zero;
        public float activeMoveSpeed;
        public float activeOrientationSharpness;
    }
}
