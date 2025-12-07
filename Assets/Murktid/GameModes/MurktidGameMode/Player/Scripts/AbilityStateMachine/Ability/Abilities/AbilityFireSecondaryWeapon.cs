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

            if(Context.playerEquipmentData.CurrentWeapon.LoadedBullets <= 0) {
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
            fireRateTimestamp = Time.time + Context.playerEquipmentData.CurrentWeapon.config.fireRate;
            didShoot = false;
            Context.animatorBridge.Shoot = true;

            Context.recoilOffset += Context.playerEquipmentData.CurrentWeapon.config.recoil;

            bool bothBarrels = !Context.input.SecondaryAction.IsPressed && Context.playerEquipmentData.CurrentWeapon.HasLoadedAmmo(2);
            if(bothBarrels) {
                Context.playerEquipmentData.CurrentWeapon.ConsumeAmmo(2);
            }
            else {
                Context.playerEquipmentData.CurrentWeapon.ConsumeAmmo(1);
            }

            Vector3 origin = Context.cameraReference.transform.position;
            Vector3 direction = Context.cameraReference.transform.forward;
            float maxDistance = Context.playerEquipmentData.CurrentWeapon.config.ADSRange;
            float radius = Context.playerEquipmentData.CurrentWeapon.config.ADSRadius;
            float damage = Context.playerEquipmentData.CurrentWeapon.config.ADSDamage;
            float randomSpeedModifier = Context.playerEquipmentData.CurrentWeapon.config.bulletVelocityRandomize;

            for(int i = 0; i < Context.shotgunCrosshair.PelletAmount; i++) {
                Ray ray = Context.shotgunCrosshair.GetPelletScreenPointRay(i, Context.cameraReference.camera);

                // spawn double pellets with origin offset left / right
                if(bothBarrels) {
                    SpawnBulletData spawnFirstBulletData = new() {
                        spawnPosition = ray.origin + -Context.motor.CharacterRight * .1f,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.CurrentWeapon.config.bulletVelocity + Random.Range(-randomSpeedModifier, randomSpeedModifier),
                        layerMask = Context.attackLayerMask,
                        damage = damage
                    };

                    Context.bulletSystem.SpawnBullet(spawnFirstBulletData);

                    SpawnBulletData spawnSecondBulletData = new() {
                        spawnPosition = ray.origin + Context.motor.CharacterRight * .1f,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.CurrentWeapon.config.bulletVelocity + Random.Range(-randomSpeedModifier, randomSpeedModifier),
                        layerMask = Context.attackLayerMask,
                        damage = damage
                    };

                    Context.bulletSystem.SpawnBullet(spawnSecondBulletData);
                }
                else {
                    SpawnBulletData spawnBulletData = new() {
                        spawnPosition = ray.origin,
                        spawnRotation = Quaternion.LookRotation(ray.direction),
                        initialVelocity = Context.playerEquipmentData.CurrentWeapon.config.bulletVelocity + Random.Range(-randomSpeedModifier, randomSpeedModifier),
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
