using UnityEngine;

namespace Murktid {

    public class PlayerCameraReference : MonoBehaviour {
        public new Camera camera;
        public Animator animator;
        public Vector3 PlanarDirection { get; set; }
        public float TargetVerticalAngle { get; set; }
        public Transform weaponHolder;

        public PlayerWeaponReference defaultPrimaryWeaponReference;
        public PlayerWeaponReference defaultSecondaryWeaponReference;

        public Transform tmpHammerTransform;
        public Transform tmpShotgunTransform;
    }
}
