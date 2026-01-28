using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Murktid {

    public class EnemySystem {

        private EnemySystemReference enemySystemReference;
        private EnemyReference enemyReferencePrefab;

        private AIDirectorReference aiDirectorReference;
        private AIDirector aiDirector;

        private List<EnemySpawnPoint> spawnPoints;
        private List<EnemyController> activeEnemies = new();

        // pool instead of destroying
        private List<EnemyController> enemiesToDestroy = new();

        public PlayerAttackerSlotSystem slotSystem;
        public CursorHandler cursorHandler;
        public PlayerController playerController;
        public StatusEffectSystem statusEffectSystem;


        public EnemySystem(CursorHandler cursorHandler, PlayerController playerController, StatusEffectSystem statusEffectSystem) {
            this.cursorHandler = cursorHandler;
            this.playerController = playerController;
            this.statusEffectSystem = statusEffectSystem;
        }

        public void Initialize() {
            enemySystemReference = Object.FindFirstObjectByType<EnemySystemReference>();
            if(enemySystemReference == null) {
                return;
            }

            enemyReferencePrefab = enemySystemReference.enemyReferencePrefab;
            spawnPoints = enemySystemReference.spawnPoints;

            aiDirectorReference = Object.FindFirstObjectByType<AIDirectorReference>();
            aiDirector = new(aiDirectorReference, this, cursorHandler, playerController, statusEffectSystem);
            aiDirector.Initialize();

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

        public void SpawnEnemy(Vector3 position, Quaternion rotation, PlayerReference targetPlayer = null) {
            EnemyReference spawnedEnemyReference = Object.Instantiate(enemyReferencePrefab, position, rotation);
            EnemyController enemyController = new(spawnedEnemyReference.context);
            enemyController.Initialize(spawnedEnemyReference);
            spawnedEnemyReference.controller = enemyController;
            activeEnemies.Add(enemyController);

            if(targetPlayer != null) {
                enemyController.SetTarget(targetPlayer);
            }
        }

        public void SpawnEnemies(int amount, bool aggressive = false) {

            PlayerReference player = null;
            if(aggressive) {
                player = Object.FindFirstObjectByType<PlayerReference>();
            }

            for(int i = 0; i < spawnPoints.Count; i++) {
                EnemySpawnPoint spawnPoint = spawnPoints[i];

                if(!spawnPoint.isActive) {
                    continue;
                }

                if(aggressive) {
                    Vector3 directionToPlayer = player.transform.position - spawnPoint.transform.position;
                    if(Physics.Raycast(spawnPoint.transform.position, directionToPlayer, out RaycastHit hit, enemySystemReference.obstacleMask) &&
                        hit.transform.TryGetComponent(out PlayerReference playerReference)) {
                        spawnPoint.isActive = false;
                        continue;
                    }
                }

                for(int j = 0; j < amount; j++) {
                    SpawnEnemy(spawnPoint.transform.position, spawnPoint.transform.rotation, player);
                }

                break;
            }

            /*for(int i = 0; i < amount; i++) {
                //EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                EnemySpawnPoint spawnPoint = spawnPoints[i % spawnPoints.Count];

                Vector3 spawnPosition = new(
                    spawnPoint.transform.position.x + Random.Range(-spawnPoint.spawnRadius, spawnPoint.spawnRadius),
                    spawnPoint.transform.position.y,
                    spawnPoint.transform.position.z + Random.Range(-spawnPoint.spawnRadius, spawnPoint.spawnRadius));

                SpawnEnemy(spawnPosition, spawnPoint.transform.rotation, player);
            }*/
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
                    slotSystem.RemoveFromActiveEnemies(enemyToDestroy);

                    // HACK (?) override this where necessary instead of manually calling?
                    // implement similar to IApplicationLifeCycle for objects that need this & destroy accordingly?
                    enemyToDestroy.Context.graphicsContainer.transform.SetParent(null);
                    enemyToDestroy.OnDestroy();
                    Object.Destroy(enemyToDestroy.Context.gameObject);


                    aiDirector.OnEnemyKilled();
                }

                enemiesToDestroy.Clear();
            }

            aiDirector?.Tick(deltaTime);
            slotSystem.Tick(activeEnemies);
        }
    }
}
