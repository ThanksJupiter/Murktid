using Murktid;
using UnityEngine;

[CreateAssetMenu(fileName = "MurktidPlayerData", menuName = "Murktid/GameMode/Murktid/PlayerData")]
public class PlayerData : ScriptableObject {
    public PlayerReference playerReferencePrefab;
    public PlayerCamera playerCameraPrefab;
}
