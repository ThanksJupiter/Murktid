using UnityEngine;
using Utils;
using KinematicCharacterController;
using UnityEngine.Serialization;

namespace Murktid {

    [System.Serializable]
    public class PlayerContext {
        [Get] public KinematicCharacterMotor motor;
        [HideInInspector] public PlayerCameraReference cameraReference;
        [Get] public Transform transform;
        public PlayerAnimatorBridge animatorBridge;
        public PlayerMovementData movementData = new();
        public PlayerEquipmentData playerEquipmentData = new();
        public PlayerSettings settings;
        public PlayerHealth health;
        public IInput input;

        public AmmoDisplay ammoDisplay;
        public BulletSystem bulletSystem;
        public LayerMask attackLayerMask;
        public ShotgunCrosshair shotgunCrosshair;

        [Header("Debug Recoil")]
        public bool hasRecoil = false;

        public float recoilOffset = 0f;
        public float recoilReturnSpeed = 5f;

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
        public float CapsuleHeight { get; set; }
        public float TargetCapsuleHeight { get; set; }
        public float TargetCameraHeight { get; set; }
        public bool IsSprinting { get; set; }
        public bool IsSprintSliding { get; set; }
        public bool IsDodging { get; set; }
        public bool IsDodgeSliding { get; set; }
        public Vector3 DodgeDirection { get; set; }
        public float CurrentFOVTarget { get; set; }
    }
}
