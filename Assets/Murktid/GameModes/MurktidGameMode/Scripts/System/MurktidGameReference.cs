using UnityEngine;

namespace Murktid {

    public class MurktidGameReference : MonoBehaviour {
        public Transform playerSpawnTransform;
        public MurktidGameData gameData;

        public Vector3 GetSpawnPosition() {
            if(SpawnPositionData.ShouldSpawnAtEditorCamera) {
                return SpawnPositionData.EditorCameraPosition;
            }

            return playerSpawnTransform.position;
        }

        public Quaternion GetSpawnRotation() {
            if(SpawnPositionData.ShouldSpawnAtEditorCamera) {
                return SpawnPositionData.EditorCameraRotation;
            }

            return playerSpawnTransform.rotation;
        }
    }
}
