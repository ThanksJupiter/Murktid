using UnityEngine;
using UnityEngine.Serialization;

namespace Murktid {

    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Murktid/GameMode/Murktid/PlayerSettings")]
    public class PlayerSettings : ScriptableObject {

        [Header("Locomotion")]
        public float defaultMoveSpeed = 6f;

        public float sprintMoveSpeed = 12f;
        public float exhaustedSprintMoveSpeed = 4.75f;
        public float sprintSlideSpeed = 18f;

        // mouse sensitivity basically, TODO do something about this (make editable in options)
        public float rotationSpeed = .15f;
        public float movementSharpness = 10f;
        public float dodgeSlideMovementSharpness = 1f;
        public float sprintSlideMovementSharpness = 3f;

        [Header("Dodge")]
        public float dodgeSpeed = 16f;

        public float dodgeDuration = .5f;

        [Header("Crouch")]
        public float standingCapsuleHeight = 2f;

        public float crouchingCapsuleHeight = 1f;
        public float capsuleRadius = .5f;
        public float capsuleHeightLerpSpeed = 5f;

        [Header("Health")]
        public float maxHealth = 100f;

        [Header("Stamina")]
        public float maxStamina = 100f;

        public float defaultStaminaRegenerationRate = 20f;
        public float defaultStaminaRegenerationDelay = .5f;
        public float sprintStaminaCost = 15f;
        public float sprintSlideStaminaCost = 10f;
        public float dodgeStaminaCost = 20f;
        public float blockPushStaminaCost = 20f;
        public float blockAttackStaminaCost = 10f;

        [Header("Jump")]
        public float jumpForce = 5f;

        public bool allowJumpingWhenSliding = true;
        public float jumpPostGroundingGraceTime = .3f;
        public float jumpPreGroundingGraceTime = .3f;

        [Header("Air Movement")]
        public Vector3 gravity = new Vector3(0f, -20f, 0f);

        [FormerlySerializedAs("airAcceleration")]
        public float airAccelerationSpeed = 8f;

        public float maxAirMoveSpeed = 4f;
        public float drag = .1f;

        [Header("Camera Settings")]
        public float cameraHeightLerpSpeed = 5f;

        public float standingCameraHeight = 1.645f;
        public float crouchingCameraHeight = .9f;
        public float minVerticalLookAngle = -85f;
        public float maxVerticalLookAngle = 85f;
        public float cameraFollowSharpness = 1000f;
        public float defaultFOV = 70f;
        public float sprintFOV = 80f;
        public float exhaustedSprintFOV = 75f;
        public float FOVLerpSpeed = 5f;

        [Header("Slot System")]
        public SlotSystemSettings slotSystemSettings;
    }
}
