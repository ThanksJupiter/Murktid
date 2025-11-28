using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Murktid {

    [System.Serializable]
    public class EnemyContext {
        [Get] public NavMeshAgent agent;
        [Get] public Rigidbody rigidbody;
        [Get] public Transform transform;
        public EnemySettings settings;

        public bool hasTarget;
    }
}
