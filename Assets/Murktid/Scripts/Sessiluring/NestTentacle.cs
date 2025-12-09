using UnityEngine;

namespace Murktid {

    public class NestTentacle : MonoBehaviour, ITarget {

        [SerializeField] private float maxHealth = 200f;
                private float currentHealth;
        [SerializeField] private BreakableRoot[] rootToBreak;
        [SerializeField] private BreakableWall[] wallsToBreak;

        private void Awake() {
            currentHealth = maxHealth;
        }
        public void Hit(float damage) {

            currentHealth -= damage;
            if(currentHealth > 0f)
                return;

            if(rootToBreak != null) {

                foreach(var wall in rootToBreak) {

                    if(wall != null) {

                        wall.Break();
                    }
                }
            }
            if(wallsToBreak != null) {

                foreach(var wall in wallsToBreak) {

                    if(wall != null) {

                        wall.Break();
                    }
                }
            }
            Destroy(gameObject);
        }
       
    }
}
