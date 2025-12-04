using UnityEngine;

namespace Murktid {

    public class NestTarget : MonoBehaviour, ITarget {

        [SerializeField] private BreakableWall[] wallsToBreak;

        public void Hit(float damage) {


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
