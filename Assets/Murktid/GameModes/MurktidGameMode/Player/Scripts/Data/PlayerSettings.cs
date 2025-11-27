using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Murktid/GameMode/Murktid/PlayerSettings")]
public class PlayerSettings : ScriptableObject {

    [Header("Locomotion")]
    public float defaultMoveSpeed = 6f;
    public float sprintMoveSpeed = 12f;

    // mouse sensitivity basically, TODO do something about this (make editable in options)
    public float rotationSpeed = .15f;
    public float movementSharpness = 10f;

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
    public float cameraHeight = 1.645f;
    public float minVerticalLookAngle = -85f;
    public float maxVerticalLookAngle = 85f;
    public float cameraFollowSharpness = 1000f;
}
