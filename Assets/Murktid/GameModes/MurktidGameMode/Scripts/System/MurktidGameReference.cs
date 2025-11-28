using Unity.AI.Navigation;
using UnityEngine;

namespace Murktid {

    public class MurktidGameReference : MonoBehaviour {
        public Transform playerSpawnTransform;
        public MurktidGameData gameData;

        public Vector3 GetSpawnPosition() {
#if UNITY_EDITOR
            if(SpawnPositionData.ShouldSpawnAtEditorCamera) {
                return SpawnPositionData.EditorCameraPosition;
            }
#endif
            return playerSpawnTransform.position;
        }

        public Quaternion GetSpawnRotation() {
#if UNITY_EDITOR
            if(SpawnPositionData.ShouldSpawnAtEditorCamera) {
                return SpawnPositionData.EditorCameraRotation;
            }
#endif
            return playerSpawnTransform.rotation;
        }
    }
}
