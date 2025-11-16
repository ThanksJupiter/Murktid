using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AutoZombie {

    public class EntryPoint : MonoBehaviour {

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize() {
            if(BootMode.BootType == BootType.UnityDefault) {
                return;
            }

            EntryPoint entryPoint = FindFirstObjectByType<EntryPoint>(FindObjectsInactive.Include);
            if(entryPoint == null) {
                AsyncOperationHandle<GameObject> handler = Addressables.InstantiateAsync("Initializer");
            }
        }

        public IEnumerator Start() {
            DontDestroyOnLoad(this);

            SceneReference activeSceneReference = FindFirstObjectByType<SceneReference>();
            if(activeSceneReference == null) {
                Debug.LogWarning("The scene has no SceneReference script. Will start by default in Splash Application State");
                GameObject sceneRef = new GameObject() {
                    name = "Scene Reference"
                };
                activeSceneReference = sceneRef.AddComponent<SceneReference>();
                activeSceneReference.applicationState = ApplicationState.Splash;
                yield return null;
            }


        }
    }
}
