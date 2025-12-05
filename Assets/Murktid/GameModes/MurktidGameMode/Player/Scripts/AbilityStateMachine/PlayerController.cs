using KinematicCharacterController;
using UnityEngine;

namespace Murktid {

    public class PlayerController : IMurktidPlayer {
        public PlayerContext Context { get; private set; }
        public StateMachine StateMachine { get; private set; }

        private readonly PlayerMovementController playerMovementController = new();
        public PlayerAbilityComponent abilityComponent;

        public PlayerController(PlayerReference playerReference) {
            Context = playerReference.context;
            abilityComponent = new(Context);
            StateMachine = new(abilityComponent);
            Context.health = new(playerReference.healthDisplayReference, playerReference.context.settings);
        }

        public void Initialize() {
            Context.animatorBridge = new(Context.cameraReference.animator);

            StateMachine.PushState<StateDefault>();
            Context.ActiveMoveSpeed = Context.settings.defaultMoveSpeed;
            playerMovementController.Initialize(Context, abilityComponent);

            if(Context.cameraReference.defaultSecondaryWeaponReference != null) {
                Context.playerEquipmentData.currentSecondaryWeapon = Context.cameraReference.defaultSecondaryWeaponReference;
                Context.playerEquipmentData.currentSecondaryWeapon.gameObject.SetActive(false);
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
            StateMachine.Tick(deltaTime);
        }

        public void Dispose() {
            Context.health.OnDestroy();
        }
    }
}
