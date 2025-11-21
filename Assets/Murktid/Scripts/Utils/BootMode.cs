using System;
using UnityEditor;
using UnityEngine;

namespace Murktid {

    public enum BootType {
        FullBoot,
        SceneBoot,
        UnityDefault
    }

    [ExecuteInEditMode]
    public class BootMode : MonoBehaviour {

        // const upper case rule yes/no?
        private const string BOOT_TYPE_KEY = "bootType"; // bootTypeKey suggested

        public static BootType BootType {
            get => (BootType)EditorPrefs.GetInt(BOOT_TYPE_KEY);
            private set {
                EditorPrefs.SetInt(BOOT_TYPE_KEY, (int)value);
                Debug.Log($"$Boot mode {BootType} set");
            }
        }

        private void OnEnable() {
            if(!EditorPrefs.HasKey(BOOT_TYPE_KEY)) {
                throw new NotImplementedException();
            }
        }

        [MenuItem("AutoZombie/&Boot/&Full Boot...", false)]
        private static void SetFullBoot() {
            BootType = BootType.FullBoot;
        }

        [MenuItem("AutoZombie/&Boot/&Scene Boot...", false)]
        private static void SetSceneBoot() {
            BootType = BootType.SceneBoot;
        }

        [MenuItem("AutoZombie/&Boot/&Unity Default No Boot...", false)]
        private static void SetDefaultBoot() {
            BootType = BootType.UnityDefault;
        }
    }
}
