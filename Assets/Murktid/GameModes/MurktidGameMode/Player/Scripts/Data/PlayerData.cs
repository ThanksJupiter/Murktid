using Murktid;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MurktidPlayerData", menuName = "Murktid/GameMode/Murktid/PlayerData")]
public class PlayerData : ScriptableObject {
    public PlayerReference playerReferencePrefab;
    [FormerlySerializedAs("playerCameraPrefab")]
    public PlayerCameraReference playerCameraReferencePrefab;
}
