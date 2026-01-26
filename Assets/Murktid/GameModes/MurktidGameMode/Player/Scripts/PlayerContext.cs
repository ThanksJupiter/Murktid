using UnityEngine;
using Utils;
using KinematicCharacterController;
using System.Collections.Generic;
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
        public PlayerStamina stamina;
        public StatusEffectSystem statusEffectSystem;
        public IInput input;

        public PlayerWeaponReference defaultRangedWeaponReferencePrefab;
        public PlayerWeaponReference defaultMeleeWeaponReferencePrefab;

        public PlayerController controller;

        public AmmoDisplay ammoDisplay;
        //public BulletSystem bulletSystem;
        public LayerMask attackLayerMask;
        public LayerMask interactionLayerMask;
        public LayerMask obstacleMask;
        public ShotgunCrosshair shotgunCrosshair;

        [Header("Status Effects")]
        public IncreaseStaminaRegenerationEffect increaseStaminaRegenerationEffect;

        [Header("Effects")]
        public ParticleSystem hammerHitEffect;

        [Header("Debug Recoil")]
        public bool hasRecoil = false;

        public float recoilOffset = 0f;
        public float recoilReturnSpeed = 5f;

        [Header("Enemy Slot System")]
        public float slotRadius = 2f;
        public int maxEngagementSlots = 5;
        public float slotArcAngle = 180f;
        public int maxAttackSlots = 3;

        public HitboxReference pushHitbox;

        public List<EnemyController> attackers = new List<EnemyController>();

        // Accessors
        public HitboxReference Hitbox => playerEquipmentData.CurrentWeapon.reference.hitbox;
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
        public bool IsAimingDownSights { get; set; }
        public bool IsShooting { get; set; }
        public bool IsReloading { get; set; }
        public bool IsBlocking { get; set; }
        public int BlockHitIndex { get; set; }
        public bool BlockPush { get; set; }
    }
}
