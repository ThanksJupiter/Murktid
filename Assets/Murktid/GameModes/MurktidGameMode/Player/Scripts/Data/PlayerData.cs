using Murktid;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MurktidPlayerData", menuName = "Murktid/GameMode/Murktid/PlayerData")]
public class PlayerData : ScriptableObject {
    public PlayerReference playerReferencePrefab;
    public NetworkObject playerNetworkObjectPrefab;
    [FormerlySerializedAs("playerCameraPrefab")]
    public PlayerCameraReference playerCameraReferencePrefab;
}
