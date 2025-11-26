using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "Murktid/GameMode/Murktid/PlayerMovementSettings")]
public class PlayerMovementSettings : ScriptableObject {

    [Header("Locomotion")]
    public float defaultMoveSpeed = 6f;

    // mouse sensitivity basically, TODO do something about this (make editable in options)
    public float rotationSpeed = .15f;
    public float movementSharpness = 10f;
    public Vector3 gravity = new Vector3(0f, -20f, 0f);

    [Header("Camera Settings")]
    public float cameraHeight = 1.645f;
    public float minVerticalLookAngle = -85f;
    public float maxVerticalLookAngle = 85f;
    public float cameraFollowSharpness = 1000f;
}
