using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Murktid {
    public static class SpawnPositionData {
        private const string spawnAtEditorCameraPosition = "spawnAtEditorCameraPosition";
        private const string editorCameraPosition = "editorCameraPosition";
        private const string editorCameraRotation = "editorCameraRotation";

        public static Vector3 With(this Vector3 origin, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? origin.x, y ?? origin.y, z ?? origin.z);
        }

        public static void SetEditorCameraSpawnData(bool enabled, Vector3 position, Quaternion rotation) {
            ShouldSpawnAtEditorCamera = enabled;
            EditorCameraPosition = position;
            EditorCameraRotation = rotation;

            /*EditorPrefs.SetFloat(editorCameraPosition + "x", position.x);
            EditorPrefs.SetFloat(editorCameraPosition + "y", position.y);
            EditorPrefs.SetFloat(editorCameraPosition + "z", position.z);

            EditorPrefs.SetFloat(editorCameraRotation + "x", rotation.x);
            EditorPrefs.SetFloat(editorCameraRotation + "y", rotation.y);
            EditorPrefs.SetFloat(editorCameraRotation + "z", rotation.z);
            EditorPrefs.SetFloat(editorCameraRotation + "w", rotation.w);*/
        }

        private static bool shouldSpawnAtEditorCamera;
        public static bool ShouldSpawnAtEditorCamera {
            get => EditorPrefs.GetBool(spawnAtEditorCameraPosition);
            set {
                shouldSpawnAtEditorCamera = value;
                EditorPrefs.SetBool(spawnAtEditorCameraPosition, shouldSpawnAtEditorCamera);
            }
        }

        private static Vector3 cameraPosition;
        public static Vector3 EditorCameraPosition {
            get {
                Vector3 position = new() {
                    x = EditorPrefs.GetFloat(editorCameraPosition + "x"),
                    y = EditorPrefs.GetFloat(editorCameraPosition + "y"),
                    z = EditorPrefs.GetFloat(editorCameraPosition + "z")
                };

                return position;
            }
            set {
                cameraPosition = value;
                EditorPrefs.SetFloat(editorCameraPosition + "x", cameraPosition.x);
                EditorPrefs.SetFloat(editorCameraPosition + "y", cameraPosition.y);
                EditorPrefs.SetFloat(editorCameraPosition + "z", cameraPosition.z);
            }
        }

        private static Quaternion cameraRotation;
        public static Quaternion EditorCameraRotation {
            get {
                Quaternion rotation = new() {
                    x = EditorPrefs.GetFloat(editorCameraRotation + "x"),
                    y = EditorPrefs.GetFloat(editorCameraRotation + "y"),
                    z = EditorPrefs.GetFloat(editorCameraRotation + "z"),
                    w = EditorPrefs.GetFloat(editorCameraRotation + "w")
                };

                return rotation;
            }
            set {
                cameraRotation = value;
                EditorPrefs.SetFloat(editorCameraRotation + "x", cameraRotation.x);
                EditorPrefs.SetFloat(editorCameraRotation + "y", cameraRotation.y);
                EditorPrefs.SetFloat(editorCameraRotation + "z", cameraRotation.z);
                EditorPrefs.SetFloat(editorCameraRotation + "w", cameraRotation.w);
            }
        }
    }

    public class SceneViewCameraTest : EditorWindow {

        [MenuItem("Window/SceneViewCameraTest")]
        static void Init() {
            // Get existing open window or if none, make a new one:
            SceneViewCameraTest window = (SceneViewCameraTest)EditorWindow.GetWindow(typeof(SceneViewCameraTest));
        }

        void OnGUI() {
            EditorGUILayout.TextField("SceneViewCameraPosition", "" + SpawnPositionData.EditorCameraPosition);
            EditorGUILayout.TextField("SceneViewCameraAngle", "" + SpawnPositionData.EditorCameraRotation);
            EditorGUILayout.TextField("Press C to spawn at camera position");

            if(SceneView.currentDrawingSceneView != null) {
                Debug.Log("hello");
            }

            if(Keyboard.current.cKey.wasPressedThisFrame) {

            }

            if(GUI.Button(new Rect(10, 70, 200, 30), "Set Spawn Position")) {
                SpawnAtSceneCameraPosition();
            }

            if(GUI.Button(new Rect(10, 100, 50, 30), "Reset")) {
                SpawnPositionData.SetEditorCameraSpawnData(false, Vector3.zero, Quaternion.identity);
            }
        }

        private void SpawnAtSceneCameraPosition() {
            Vector3 spawnPosition = SceneView.lastActiveSceneView.camera.transform.position;
            Vector3 flatRotation = SceneView.lastActiveSceneView.camera.transform.forward.With(y: 0f);
            Quaternion spawnRotation = Quaternion.LookRotation(flatRotation);

            SpawnPositionData.SetEditorCameraSpawnData(true, spawnPosition, spawnRotation);
            EditorApplication.EnterPlaymode();
        }
    }
}
