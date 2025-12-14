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
            StateMachine.Tick(deltaTime);
        }

        public void OnDestroy() {
            abilityComponent.StopAllAbilities();
        }
    }
}
