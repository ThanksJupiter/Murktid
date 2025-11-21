using System;
using UnityEditor;
using UnityEngine;

namespace Murktid {

    public class SceneReference : MonoBehaviour {
        public ApplicationState applicationState = ApplicationState.Invalid;
        public GameMode gameMode;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SceneReference))]
    public class SceneAuthoringEditor : Editor {
        private SceneReference sceneReference;

        private SerializedProperty appState;
        private SerializedProperty gameMode;

        private void OnEnable() {
            sceneReference = (SceneReference)target;
            appState = serializedObject.FindProperty("applicationState");
            gameMode = serializedObject.FindProperty("gameMode");
        }

        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField(appState, new GUIContent("Application State"));
            if(sceneReference.applicationState == ApplicationState.GameMode) {
                EditorGUILayout.PropertyField(gameMode, new GUIContent("Game Mode"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
