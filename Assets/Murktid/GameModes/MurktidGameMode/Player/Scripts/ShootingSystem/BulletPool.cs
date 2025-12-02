using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class BulletPool {
        private List<BulletReference> availableBullets = new List<BulletReference>();
        public BulletReference bulletReferencePrefab;

        public BulletReference GetAvailableBullet() {
            foreach(BulletReference bullet in availableBullets) {
                if(!bullet.data.isActive) {
                    return bullet;
                }
            }

            BulletReference newBulletReference = Object.Instantiate(bulletReferencePrefab);
            BulletData newBulletData = newBulletReference.data;
            newBulletData.transform = newBulletReference.transform;
            return newBulletReference;
        }

        public void ReturnBulletToPool(BulletReference bulletReference) {

            bulletReference.data.isActive = false;
            bulletReference.gameObject.SetActive(false);
            availableBullets.Add(bulletReference);
        }
    }
}
