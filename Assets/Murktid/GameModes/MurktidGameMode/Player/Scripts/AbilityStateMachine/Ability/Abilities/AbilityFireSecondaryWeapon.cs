using UnityEngine;

namespace Murktid {

    public class AbilityFireSecondaryWeapon : PlayerAbility {

        private bool didShoot = false;

        public override bool ShouldActivate() {
            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return false;
            }

            if(!Context.input.PrimaryAction.wasPressedThisFrame) {
                return false;
            }

            return true;
        }

        public override bool ShouldDeactivate() {

            /*if(!Context.input.SecondaryAction.IsPressed) {
                return true;
            }

            if(Context.playerEquipmentData.currentWeaponType != WeaponType.Secondary) {
                return true;
            }*/

            return didShoot;

            //return false;
        }

        private float fireRateTimestamp = -float.MaxValue;

        protected override void Tick(float deltaTime) {
            if(Time.time >= fireRateTimestamp) {
                didShoot = true;
            }
        }

        protected override void OnActivate() {

            fireRateTimestamp = Time.time + Context.playerEquipmentData.currentWeapon.weaponData.fireRate;
            didShoot = false;

            Context.recoilOffset += Context.playerEquipmentData.currentWeapon.weaponData.recoil;

            Vector3 origin = Context.cameraReference.transform.position;
            Vector3 direction = Context.cameraReference.transform.forward;
            float maxDistance = Context.playerEquipmentData.currentWeapon.weaponData.ADSRange;
            float radius = Context.playerEquipmentData.currentWeapon.weaponData.ADSRadius;
            float damage = Context.playerEquipmentData.currentWeapon.weaponData.ADSDamage;

            for(int i = 0; i < Context.shotgunCrosshair.PelletAmount; i++) {
                Ray ray = Context.shotgunCrosshair.GetPelletScreenPointRay(i, Context.cameraReference.camera);
                //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 5f);

                // spawn bullet
                SpawnBulletData spawnBulletData = new() {
                    //spawnPosition = Context.playerEquipmentData.currentWeapon.weaponData.firePoint.position,
                    spawnPosition = ray.origin + direction,
                    spawnRotation = Quaternion.LookRotation(ray.direction),
                    initialVelocity = Context.playerEquipmentData.currentWeapon.weaponData.bulletVelocity,
                    layerMask = Context.attackLayerMask,
                    damage = damage
                };

                Context.bulletSystem.SpawnBullet(spawnBulletData);

                /*RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, Context.attackLayerMask);
                for(int j = 0; j < hits.Length; j++) {
                    if(!hits[j].transform.TryGetComponent(out ITarget target)) {
                        continue;
                    }

                    target.Hit(damage);
                    Context.shotgunCrosshair.DisplayPelletHit(i);
                }*/
            }

            // shooty
            // take a boolie from the pool
            // bring it in
            // find out what it hits
            // Bullet bullet = BulletPool.GetBullet(shooty data)
        }
    }
}
