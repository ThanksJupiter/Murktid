using UnityEngine;

namespace Murktid {

    public class NestTarget : MonoBehaviour, ITarget {

        [SerializeField] private Transform healthBarPivot;
       [SerializeField] private Vector3 healthBarOffset = new Vector3 (0f, 2f, 0f);

        [SerializeField] private float maxHealth = 500f;
        private float currentHealth;

        [SerializeField] private BreakableWall[] wallsToBreak;
        

        private void Awake() {
            currentHealth = maxHealth;
        }

        private void UpdateHealthBar() {
            if(healthBarPivot == null) return;

            float normalized = currentHealth / maxHealth;
            healthBarPivot.localScale = new Vector3(normalized, 1f, 1f);

           if (Camera.main != null)
                healthBarPivot.LookAt(Camera.main.transform);
        }

        public void Hit(float damage) {

            currentHealth -= damage;
            UpdateHealthBar();

            if(currentHealth > 0f)
                return;

            //Debug.Log($"Hit: { gameObject.name } for { damage }");
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
