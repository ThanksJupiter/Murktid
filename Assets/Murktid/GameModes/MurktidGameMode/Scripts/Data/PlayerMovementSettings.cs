using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "Murktid/GameMode/Murktid/PlayerMovementSettings")]
public class PlayerMovementSettings : ScriptableObject {

    [Header("Locomotion")]
    public float defaultMoveSpeed = 6f;
    public float movementSharpness = 10f;
    public Vector3 gravity = new Vector3(0f, -20f, 0f);
}
