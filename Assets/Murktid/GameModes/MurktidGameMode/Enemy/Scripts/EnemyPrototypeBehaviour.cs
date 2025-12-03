using UnityEngine;

namespace Murktid {

    [System.Serializable]
    public class EnemyPrototypeBehaviour {
        public MeshRenderer eyeMeshRenderer;
        public MeshRenderer leftArmMeshRenderer;
        public MeshRenderer rightArmMeshRenderer;

        public Material idleMaterial;
        public Material chaseMaterial;
        public Material attackMaterial;

        public Rigidbody eyeRigidbody;
        public Rigidbody leftArmRigidbody;
        public Rigidbody rightArmRigidbody;

        public BoxCollider eyeBoxCollider;
        public BoxCollider leftArmBoxCollider;
        public BoxCollider rightArmBoxCollider;

        public float explodeVelocity = 5f;

        public void SetIdleMaterials() {
            eyeMeshRenderer.material = idleMaterial;
            leftArmMeshRenderer.material = idleMaterial;
        }

        public void SetChaseMaterials() {

        }

        public void SetAttackMaterials() {

        }

        public void Explode() {
            PlayerReference playerReference = Object.FindFirstObjectByType<PlayerReference>();
            Vector3 explodeDirection = playerReference.transform.forward;
            Vector3 explodeForce = explodeDirection * explodeVelocity;

            eyeRigidbody.transform.SetParent(null);
            leftArmRigidbody.transform.SetParent(null);
            rightArmRigidbody.transform.SetParent(null);

            eyeBoxCollider.enabled = true;
            leftArmBoxCollider.enabled = true;
            rightArmBoxCollider.enabled = true;

            eyeRigidbody.useGravity = true;
            leftArmRigidbody.useGravity = true;
            rightArmRigidbody.useGravity = true;

            eyeRigidbody.constraints = RigidbodyConstraints.None;
            leftArmRigidbody.constraints = RigidbodyConstraints.None;
            rightArmRigidbody.constraints = RigidbodyConstraints.None;

            eyeRigidbody.linearVelocity = explodeForce;
            leftArmRigidbody.linearVelocity = explodeForce;
            rightArmRigidbody.linearVelocity = explodeForce;
        }
    }
}
