using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace Murktid {

    public class EnemySystemReference : MonoBehaviour {
        public NavMeshSurface navMeshSurface;
        public EnemyReference enemyReferencePrefab;
        public List<EnemySpawnPoint> spawnPoints;
        public LayerMask obstacleMask;
    }
}
