using UnityEngine;

namespace Murktid {

    public class EnemyAbilityComponent : AbilityComponent {
        public EnemyContext Context { get; private set; }

        public EnemyAbilityComponent(EnemyContext context) {
            Context = context;
        }
    }
}
