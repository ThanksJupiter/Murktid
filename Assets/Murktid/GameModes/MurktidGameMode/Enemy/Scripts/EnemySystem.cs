using UnityEngine;

namespace Murktid {

    public class EnemySystem {

        private EnemySystemReference enemySystemReference;
        private EnemyReference enemyReferencePrefab;

        public EnemySystem() {

        }

        public void Initialize() {
            enemySystemReference = Object.FindFirstObjectByType<EnemySystemReference>();
            if(enemySystemReference == null) {
                return;
            }

            enemyReferencePrefab = enemySystemReference.enemyReferencePrefab;
        }

        public void Tick(float deltaTime) {

        }
    }
}
