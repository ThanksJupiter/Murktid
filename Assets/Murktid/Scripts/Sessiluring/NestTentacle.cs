using UnityEngine;

namespace Murktid {

    public class NestTentacle : MonoBehaviour, ITarget {

        [SerializeField] private float maxHealth = 200f;
                private float currentHealth;

        private void Awake() {
            currentHealth = maxHealth;
        }
        public void Hit(float damage) {

            currentHealth -= damage;
            if(currentHealth > 0f)
                return;

            Destroy(gameObject);
        }
       
    }
}
