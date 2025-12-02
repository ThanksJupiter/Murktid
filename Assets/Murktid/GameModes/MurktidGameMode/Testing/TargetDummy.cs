using UnityEngine;

namespace Murktid {

    public class TargetDummy : MonoBehaviour, ITarget {

        public void Hit(float damage) {
            //Debug.Log($"Hit: { gameObject.name } for { damage }");
        }
    }
}
