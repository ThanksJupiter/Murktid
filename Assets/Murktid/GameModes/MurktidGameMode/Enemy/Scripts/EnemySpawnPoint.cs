using System;
using UnityEngine;

namespace Murktid {

    [SelectionBase]
    public class EnemySpawnPoint : MonoBehaviour {
        public GameObject mesh;
        public int enemiesToSpawn = 1;
        public float spawnRadius = 2f;
        public bool isActive = true;

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
