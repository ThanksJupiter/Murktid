using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    [System.Serializable]
    public class BulletData {
        public Vector3 position;
        public Vector3 forward;
        public bool isActive;
        public float speed;
        public bool hasHitTarget;
        public ITarget hitTarget;
        public IMurktidPlayer player;
        public float bulletDespawnTimestamp;
        public bool shouldBeReturnedToPool;
        public float damage;
        public LayerMask layerMask;
        public GameObject hitEffectPrefab;

        public bool ShouldBeReturnedToPool => Time.time > bulletDespawnTimestamp || shouldBeReturnedToPool;
    }

    public class SpawnBulletData {
        public Vector3 spawnPosition;
        public Vector3 velocityDirection;
        public float initialVelocity;
        public LayerMask layerMask;
        public GameObject hitEffectPrefab;
        public float damage;
    }

    public class BulletSystem {
        private BulletPool bulletPool;
        private BloodEffectSystem bloodEffectSystem;
        private List<BulletData> activeBullets = new List<BulletData>();

        public void Initialize(MurktidGameReference gameReference) {
            bulletPool = new();
            bloodEffectSystem = new();
        }

        public void SpawnBullet(SpawnBulletData data) {
            BulletData bulletData = bulletPool.GetAvailableBullet();

            bulletData.speed = data.initialVelocity;
            bulletData.position = data.spawnPosition;
            bulletData.forward = data.velocityDirection;
            bulletData.bulletDespawnTimestamp = Time.time + 10f;
            bulletData.damage = data.damage;
            bulletData.isActive = true;
            bulletData.shouldBeReturnedToPool = false;
            bulletData.layerMask = data.layerMask;
            bulletData.hitEffectPrefab = data.hitEffectPrefab;

            activeBullets.Add(bulletData);
        }

        public void Tick(float deltaTime) {
            for(int index = activeBullets.Count - 1; index >= 0; index--) {
                BulletData bullet = activeBullets[index];
                if(!bullet.isActive) {
                    continue;
                }

                float bulletVelocity = bullet.speed * deltaTime;
                if(DidBulletHit(bullet, bulletVelocity, out ITarget target)) {
                    if(target != null) {
                        OnBulletHitTarget(bullet, target);
                    }
                }

                if(bullet.ShouldBeReturnedToPool) {
                    ReturnBulletToPool(bullet);
                    continue;
                }

                MoveBullet(bullet, bulletVelocity);
            }
        }

        private void MoveBullet(BulletData bullet, float bulletVelocity) {
            bullet.position += bullet.forward * bulletVelocity;
        }

        private bool DidBulletHit(BulletData bullet, float bulletVelocity, out ITarget target) {

            target = null;
            if(!Physics.Raycast(bullet.position, bullet.forward, out RaycastHit hitInfo, bulletVelocity, bullet.layerMask)) {
                return false;
            }

            if(hitInfo.transform.TryGetComponent(out ITarget hitTarget)) {
                target = hitTarget;
                return true;
            }

            //Debug.Log($"bullet hit, play particle effect pls");
            //bullet.hitParticleSystem.Play();
            bullet.shouldBeReturnedToPool = true;
            bullet.speed = 0f;

            return true;
        }

        private void OnBulletHitTarget(BulletData bullet, ITarget target) {
            target.Hit(bullet.damage);
            bullet.shouldBeReturnedToPool = true;

            bloodEffectSystem.PlayBloodSpatterEffect(bullet.position, Quaternion.LookRotation(bullet.forward, Vector3.up), bullet.hitEffectPrefab);
        }

        private void ReturnBulletToPool(BulletData bullet) {
            activeBullets.Remove(bullet);
            bulletPool.ReturnBulletToPool(bullet);
        }
    }
}
