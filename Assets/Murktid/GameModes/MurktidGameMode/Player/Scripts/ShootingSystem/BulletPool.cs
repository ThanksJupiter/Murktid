using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class BulletPool {
        private List<BulletData> availableBullets = new List<BulletData>();

        public BulletData GetAvailableBullet() {
            foreach(BulletData bullet in availableBullets) {
                if(!bullet.isActive) {
                    return bullet;
                }
            }

            BulletData newBullet = new();
            return newBullet;
        }

        public void ReturnBulletToPool(BulletData bullet) {
            bullet.isActive = false;
            if(!availableBullets.Contains(bullet)) {
                availableBullets.Add(bullet);
            }
        }
    }
}
