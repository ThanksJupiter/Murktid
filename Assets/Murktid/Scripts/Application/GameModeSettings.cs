using Murktid;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

// this is in no namespace, on purpose?

[CreateAssetMenu(fileName = "GameModeSettings", menuName = "Murktid/Settings/General/GameModeSettings")]
public class GameModeSettings : ScriptableObject {
    public GameModeData[] gameModeData;
}

[Serializable]
public class GameModeData {
    public GameMode gameMode;
    public AssetReference scene;
}
