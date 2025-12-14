using UnityEngine;

namespace Murktid {

    public enum EHitboxType {
        Sphere,
        Box
    }

    public class HitboxReference : MonoBehaviour {

        public EHitboxType hitboxType = EHitboxType.Sphere;

        public Vector3 Center => transform.position;
        public Quaternion Rotation => transform.rotation;
        public float size;
        public Vector3 halfExtents = Vector3.one;

        public bool isActive = false;

        private readonly Vector3[] corners = new Vector3[8];
        private readonly Vector3[] localCorners = new Vector3[]
        {
            new Vector3(-1, -1, -1), // 0: bottom-left-back
            new Vector3( 1, -1, -1), // 1: bottom-right-back
            new Vector3( 1, -1,  1), // 2: bottom-right-front
            new Vector3(-1, -1,  1), // 3: bottom-left-front
            new Vector3(-1,  1, -1), // 4: top-left-back
            new Vector3( 1,  1, -1), // 5: top-right-back
            new Vector3( 1,  1,  1), // 6: top-right-front
            new Vector3(-1,  1,  1)  // 7: top-left-front
        };

        private void OnDrawGizmos() {

            Gizmos.color = isActive ? Color.red : Color.blue;

            switch(hitboxType) {

                case EHitboxType.Sphere:
                    Gizmos.DrawWireSphere(Center, size);
                    break;
                case EHitboxType.Box:
                    for (int i = 0; i < 8; i++)
                    {
                        Vector3 localPoint = Vector3.Scale(localCorners[i], halfExtents);
                        corners[i] = Center + Rotation * localPoint;
                    }

                    // Draw bottom face
                    Gizmos.DrawLine(corners[0], corners[1]);
                    Gizmos.DrawLine(corners[1], corners[2]);
                    Gizmos.DrawLine(corners[2], corners[3]);
                    Gizmos.DrawLine(corners[3], corners[0]);

                    // Draw top face
                    Gizmos.DrawLine(corners[4], corners[5]);
                    Gizmos.DrawLine(corners[5], corners[6]);
                    Gizmos.DrawLine(corners[6], corners[7]);
                    Gizmos.DrawLine(corners[7], corners[4]);

                    // Draw vertical edges
                    Gizmos.DrawLine(corners[0], corners[4]);
                    Gizmos.DrawLine(corners[1], corners[5]);
                    Gizmos.DrawLine(corners[2], corners[6]);
                    Gizmos.DrawLine(corners[3], corners[7]);
                    break;
            }
        }
    }
}
