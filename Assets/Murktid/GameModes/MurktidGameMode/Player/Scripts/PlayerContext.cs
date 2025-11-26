using UnityEngine;
using Utils;
using KinematicCharacterController;

namespace Murktid {

    [System.Serializable]
    public class PlayerContext {
        [Get] public KinematicCharacterMotor motor;
        [HideInInspector] public PlayerCamera camera;
        [Get] public Transform transform;
        public PlayerMovement movement = new PlayerMovement();
        public PlayerMovementSettings settings;
        public IInput input;
    }
}
