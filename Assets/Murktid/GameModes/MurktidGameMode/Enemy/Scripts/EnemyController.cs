using UnityEngine;

namespace Murktid {

    public class EnemyController {
        public EnemyContext Context { get; private set; }
        public StateMachine StateMachine { get; private set; }

        private readonly EnemyAbilityComponent abilityComponent;

        public EnemyController(EnemyContext context) {
            Context = context;
            abilityComponent = new(context);
            StateMachine = new(abilityComponent);
        }

        public void Initialize(EnemyReference enemyReference) {
            Context.controller = this;
            Context.animatorBridge = new(enemyReference.animator);
            Context.gameObject = enemyReference.gameObject;
            Context.health = new(enemyReference);
            StateMachine.PushState<EnemyStateDefault>();
        }

        public void Tick(float deltaTime) {

            Context.animatorBridge.Speed = Context.agent.desiredVelocity.magnitude / Context.settings.defaultChaseSpeed;
            if(Context.animatorBridge.IsInDamageLayer) {
                Context.animatorBridge.AttackReady = false;
                Context.animatorBridge.TakeDamage = false;
                Context.animatorBridge.IsKnockback = false;
            }
            StateMachine.Tick(deltaTime);
        }

        public void SetTarget(PlayerReference player) {
            Context.targetPlayer = player;
            Context.playerSlotSystem = player.context.controller.attackerSlotSystem;
            // add self to slot system for consideration
            Context.playerSlotSystem.AddToActiveEnemies(Context.controller);
        }

        public void StopAttacking() {
            Context.hasAttackSlot = false;
            Context.animatorBridge.IsAttacking = false;
            Context.animatorBridge.AttackReady = false;
        }

        public void OnDestroy() {
            abilityComponent.StopAllAbilities();
        }
    }
}
