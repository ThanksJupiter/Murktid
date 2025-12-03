using UnityEngine;

namespace Murktid {

    public class NestTarget : MonoBehaviour, ITarget {

        public void Hit(float damage) {

            Debug.Log($"Hit: { gameObject.name } for { damage }");
        }
    }
}
