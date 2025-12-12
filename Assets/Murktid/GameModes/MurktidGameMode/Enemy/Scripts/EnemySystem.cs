using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Murktid {

    public class EnemySystem {

        private EnemySystemReference enemySystemReference;
        private EnemyReference enemyReferencePrefab;

        private List<EnemySpawnPoint> spawnPoints;
        private List<EnemyController> activeEnemies = new();

        // pool instead of destroying
        private List<EnemyController> enemiesToDestroy = new();

        public EnemySystem() {

        }

        public void Initialize() {
            enemySystemReference = Object.FindFirstObjectByType<EnemySystemReference>();
            if(enemySystemReference == null) {
                return;
            }

            enemyReferencePrefab = enemySystemReference.enemyReferencePrefab;
            spawnPoints = Object.FindObjectsByType<EnemySpawnPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            SpawnInitialEnemies();
        }

        private void SpawnInitialEnemies() {
            foreach(EnemySpawnPoint spawnPoint in spawnPoints) {
                for(int i = 0; i < spawnPoint.enemiesToSpawn; i++) {
                    spawnPoint.mesh.SetActive(false);

                    Vector3 spawnPosition = new(
                        spawnPoint.transform.position.x + Random.Range(-spawnPoint.spawnRadius, spawnPoint.spawnRadius),
                        spawnPoint.transform.position.y,
                        spawnPoint.transform.position.z + Random.Range(-spawnPoint.spawnRadius, spawnPoint.spawnRadius));

                    SpawnEnemy(spawnPosition, spawnPoint.transform.rotation);
                }
            }
        }

        private void SpawnEnemy(Vector3 position, Quaternion rotation) {
            EnemyReference spawnedEnemyReference = Object.Instantiate(enemyReferencePrefab, position, rotation);
            EnemyController enemyController = new(spawnedEnemyReference.context);
            enemyController.Initialize(spawnedEnemyReference);
            spawnedEnemyReference.controller = enemyController;
            activeEnemies.Add(enemyController);
        }

        public void Tick(float deltaTime) {

            bool destroyKilledEnemies = false;

            foreach(EnemyController enemy in activeEnemies) {
                enemy.Tick(deltaTime);

                if(enemy.Context.isDead) {
                    enemiesToDestroy.Add(enemy);
                    destroyKilledEnemies = true;
                }
            }

            if(destroyKilledEnemies) {
                for(int i = enemiesToDestroy.Count - 1; i >= 0; i--) {
                    EnemyController enemyToDestroy = enemiesToDestroy[i];
                    activeEnemies.Remove(enemyToDestroy);

                    // HACK (?) override this where necessary instead of manually calling?
                    // implement similar to IApplicationLifeCycle for objects that need this & destroy accordingly?
                    enemyToDestroy.OnDestroy();
                    Object.Destroy(enemyToDestroy.Context.gameObject);
                }

                enemiesToDestroy.Clear();
            }
        }
    }
}
