using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    [System.Serializable]
    public class BulletData {
        public Transform transform;
        public bool isActive;
        public float speed;
        public bool hasHitTarget;
        public ITarget hitTarget;
        public IMurktidPlayer player;
        public float bulletDespawnTimestamp;
        public bool shouldBeReturnedToPool;
        public float damage;
        public LayerMask layerMask;

        public bool ShouldBeReturnedToPool => Time.time > bulletDespawnTimestamp || shouldBeReturnedToPool;
    }

    public class SpawnBulletData {
        public Vector3 spawnPosition;
        public Quaternion spawnRotation;
        public float initialVelocity;
        public LayerMask layerMask;

        public float damage;
    }

    public class BulletSystem {
        private BulletPool bulletPool;
        private List<BulletReference> activeBullets = new List<BulletReference>();

        public void Initialize(MurktidGameReference gameReference) {
            bulletPool = new();
            bulletPool.bulletReferencePrefab = gameReference.gameData.bulletReferencePrefab;
        }

        public void SpawnBullet(SpawnBulletData data) {
            BulletReference spawnedBulletReference = bulletPool.GetAvailableBullet();

            spawnedBulletReference.data.speed = data.initialVelocity;
            spawnedBulletReference.transform.position = data.spawnPosition;
            spawnedBulletReference.transform.rotation = data.spawnRotation;
            spawnedBulletReference.data.bulletDespawnTimestamp = Time.time + 10f;
            spawnedBulletReference.data.damage = data.damage;
            spawnedBulletReference.data.isActive = true;
            spawnedBulletReference.data.layerMask = data.layerMask;
            spawnedBulletReference.gameObject.SetActive(true);

            activeBullets.Add(spawnedBulletReference);
        }

        public void Tick(float deltaTime) {
            for(int index = activeBullets.Count - 1; index >= 0; index--) {
                BulletReference bullet = activeBullets[index];
                if(!bullet.data.isActive) {
                    continue;
                }

                float bulletVelocity = bullet.data.speed * deltaTime;
                if(DidBulletHit(bullet, bulletVelocity, out ITarget target)) {
                    if(target != null) {
                        OnBulletHitTarget(bullet, target);
                    }
                }

                if(bullet.data.ShouldBeReturnedToPool) {
                    ReturnBulletToPool(bullet);
                    continue;
                }

                MoveBullet(bullet, bulletVelocity);
            }
        }

        private void MoveBullet(BulletReference bullet, float bulletVelocity) {
            bullet.transform.position += bullet.transform.forward * bulletVelocity;
        }

        private bool DidBulletHit(BulletReference bullet, float bulletVelocity, out ITarget target) {

            target = null;
            if(!Physics.Raycast(bullet.transform.position, bullet.transform.forward, out RaycastHit hitInfo, bulletVelocity, bullet.data.layerMask)) {
                return false;
            }

            if(hitInfo.transform.TryGetComponent(out ITarget hitTarget)) {
                target = hitTarget;
                return true;
            }

            // hit walls & get recycled

            return false;
        }

        private void OnBulletHitTarget(BulletReference bullet, ITarget target) {
            target.Hit(bullet.data.damage);
            //bullet.data.speed = 0f;
            bullet.data.bulletDespawnTimestamp = Time.time + 2f;
            bullet.hitParticleSystem.Play();
        }

        private void ReturnBulletToPool(BulletReference bulletReference) {
            activeBullets.Remove(bulletReference);
            bulletPool.ReturnBulletToPool(bulletReference);
        }
    }
}
