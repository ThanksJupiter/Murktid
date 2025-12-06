using KinematicCharacterController;
using UnityEngine;

namespace Murktid {

    public class PlayerController : IMurktidPlayer {
        public PlayerContext Context { get; private set; }
        public StateMachine StateMachine { get; private set; }

        private readonly PlayerMovementController playerMovementController = new();
        public PlayerAbilityComponent abilityComponent;

        private PlayerWeaponSystem weaponSystem;

        public PlayerController(PlayerReference playerReference) {
            Context = playerReference.context;
            abilityComponent = new(Context);
            StateMachine = new(abilityComponent);
            Context.health = new(playerReference.healthDisplayReference, playerReference.context.settings);
            Context.ammoDisplay = new(playerReference);
            weaponSystem = new();
            weaponSystem.Initialize(playerReference);
        }

        public void Initialize() {
            Context.animatorBridge = new(Context.cameraReference.animator);

            StateMachine.PushState<StateDefault>();
            Context.ActiveMoveSpeed = Context.settings.defaultMoveSpeed;
            Context.TargetCameraHeight = Context.settings.standingCameraHeight;
            Context.TargetCapsuleHeight = Context.settings.standingCapsuleHeight;
            playerMovementController.Initialize(Context, abilityComponent);

            if(Context.cameraReference.defaultSecondaryWeaponReference != null) {

                PlayerWeaponData newWeaponData = new(Context.cameraReference.defaultSecondaryWeaponReference);
                Context.playerEquipmentData.currentSecondaryWeapon = newWeaponData;
                Context.playerEquipmentData.currentSecondaryWeapon.reference.gameObject.SetActive(false);
            }
        }

        public void SetInput() {
            /*Vector3 moveInputVector = new(Context.input.Move.value.x, 0f, Context.input.Move.value.y);
            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.forward, Context.motor.CharacterUp).normalized;

            if(cameraPlanarDirection.sqrMagnitude == 0f) {
                cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.up, Context.motor.CharacterUp).normalized;
            }

            Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, Context.motor.CharacterUp);
            Context.movementData.moveInputVector = cameraPlanarRotation * moveInputVector;*/
        }
        public void Tick(float deltaTime) {

            Context.CapsuleHeight = Mathf.Lerp(Context.CapsuleHeight, Context.TargetCapsuleHeight, 1f - Mathf.Exp(-Context.settings.capsuleHeightLerpSpeed * deltaTime));
            Context.motor.SetCapsuleDimensions(Context.settings.capsuleRadius, Context.CapsuleHeight, Context.CapsuleHeight * .5f);

            StateMachine.Tick(deltaTime);
        }

        public void Dispose() {
            Context.health.OnDestroy();
        }
    }
}
