using UnityEngine;
using Utils;
using KinematicCharacterController;
using UnityEngine.Serialization;

namespace Murktid {

    [System.Serializable]
    public class PlayerContext {
        [Get] public KinematicCharacterMotor motor;
        [FormerlySerializedAs("camera")]
        [HideInInspector] public PlayerCameraReference cameraReference;
        [Get] public Transform transform;
        public PlayerMovementData movementData = new();
        public PlayerWeaponData playerWeaponData = new();
        public PlayerSettings settings;
        public IInput input;

        // Accessors
        public KinematicCharacterMotor Motor => motor;
        public Vector3 MoveInputVector { get => movementData.moveInputVector; set => movementData.moveInputVector = value; }
        public bool IsGrounded => motor.GroundingStatus.IsStableOnGround;
        public bool FoundAnyGround => motor.GroundingStatus.FoundAnyGround;
        public Vector3 Velocity => motor.Velocity;
        public Vector3 BaseVelocity => motor.BaseVelocity;
        public Vector3 CharacterUp => motor.CharacterUp;
        public Vector3 GroundNormal => motor.GroundingStatus.GroundNormal;
        public float TimeSinceGrounded => Time.time - movementData.lastGroundedTimestamp;
        public float ActiveMoveSpeed {
            get => movementData.activeMoveSpeed;
            set => movementData.activeMoveSpeed = value;
        }
    }
}
