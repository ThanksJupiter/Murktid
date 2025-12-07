using System.Collections.Generic;
using UnityEngine;

namespace Murktid {
    public static class GameplayStatics {

        public static EnemyController[] GetEnemiesAtPositionWithinRadius(Vector3 position, float radius, LayerMask layerMask) {

            List<EnemyController> overlappedEnemies = new();
            Collider[] overlappedColliders = Physics.OverlapSphere(position, radius, layerMask);

            foreach (Collider collider in overlappedColliders)
            {
                if(!collider.TryGetComponent(out EnemyReference enemyReference)) {
                    continue;
                }

                overlappedEnemies.Add(enemyReference.controller);
            }

            return overlappedEnemies.ToArray();
        }
    }
}
