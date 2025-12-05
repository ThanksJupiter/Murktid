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

            if(Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets <= 0) {
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

        private float fireRateTimestamp = float.MinValue;

        protected override void Tick(float deltaTime) {
            if(Time.time >= fireRateTimestamp) {
                didShoot = true;
            }

            if(Context.animatorBridge.IsInRangedFireLayer && didShoot) {
                Context.animatorBridge.Shoot = false;
            }
        }

        protected override void OnActivate() {
            fireRateTimestamp = Time.time + Context.playerEquipmentData.currentWeapon.weaponData.fireRate;
            didShoot = false;
            Context.animatorBridge.Shoot = true;

            Context.recoilOffset += Context.playerEquipmentData.currentWeapon.weaponData.recoil;

            bool bothBarrels = !Context.input.SecondaryAction.IsPressed && Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets >= 2;
            if(bothBarrels) {
                Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets -= 2;
            }
            else {
                Context.playerEquipmentData.currentWeapon.weaponData.loadedBullets -= 1;
            }

            Vector3 origin = Context.cameraReference.transform.position;
            Vector3 direction = Context.cameraReference.transform.forward;
            float maxDistance = Context.playerEquipmentData.currentWeapon.weaponData.ADSRange;
            float radius = Context.playerEquipmentData.currentWeapon.weaponData.ADSRadius;
            float damage = Context.playerEquipmentData.currentWeapon.weaponData.ADSDamage;

            for(int i = 0; i < Context.shotgunCrosshair.PelletAmount; i++) {
                Ray ray = Context.shotgunCrosshair.GetPelletScreenPointRay(i, Context.cameraReference.camera);
                //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 5f);

                if(bothBarrels) {
                    // spawn two pellets per ray with left / right offset
                    Debug.DrawRay(ray.origin + -Context.motor.CharacterRight * .1f, ray.direction * radius, Color.blue, 5f);
                    Debug.DrawRay(ray.origin + Context.motor.CharacterRight * .1f, ray.direction * radius, Color.red, 5f);

                    SpawnBulletData spawnFirstBulletData = new() {
                        spawnPosition = ray.origin + -Context.motor.CharacterRight * .1f,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.currentWeapon.weaponData.bulletVelocity,
                        layerMask = Context.attackLayerMask,
                        damage = damage
                    };

                    Context.bulletSystem.SpawnBullet(spawnFirstBulletData);

                    SpawnBulletData spawnSecondBulletData = new() {
                        spawnPosition = ray.origin + Context.motor.CharacterRight * .1f,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.currentWeapon.weaponData.bulletVelocity,
                        layerMask = Context.attackLayerMask,
                        damage = damage
                    };

                    Context.bulletSystem.SpawnBullet(spawnSecondBulletData);
                }
                else {
                    SpawnBulletData spawnBulletData = new() {
                        spawnPosition = ray.origin,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.currentWeapon.weaponData.bulletVelocity,
                        layerMask = Context.attackLayerMask,
                        damage = damage
                    };

                    Context.bulletSystem.SpawnBullet(spawnBulletData);
                }
            }
        }

        protected override void OnDeactivate() {
            Context.animatorBridge.Shoot = false;
        }
    }
}
