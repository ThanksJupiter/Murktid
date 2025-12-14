using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class PlayerAttackerSlotSystem : PlayerSystem {

        public HashSet<int> ActiveEngagementSlots { get; } = new HashSet<int>();
        public HashSet<int> ActiveAttackSlots { get; } = new HashSet<int>();

        private float internalRequestTimer = .5f;

        public PlayerAttackerSlotSystem(PlayerContext context) : base(context) {
            //occupiedSlots = new bool[context.maxAttackers];
        }

        public void Tick(float deltaTime) {
            internalRequestTimer -= deltaTime;
        }

        public bool TryClaimEngagementSlot(Vector3 enemyPosition, out int claimedIndex)
        {
            claimedIndex = -1;

            float bestScore = float.NegativeInfinity;

            for (int i = 0; i < context.maxEngagementSlots; i++)
            {
                if (ActiveEngagementSlots.Contains(i))
                    continue;

                Vector3 slotPosition = GetSlotPosition(i);
                Vector3 dir = (slotPosition - context.transform.position).normalized;
                float dot = Vector3.Dot(context.transform.forward, dir);
                float distance = Vector3.Distance(enemyPosition, slotPosition);
                float score = dot - (distance * 0.1f);

                if (score > bestScore)
                {
                    bestScore = score;
                    claimedIndex = i;
                }
            }

            if (claimedIndex != -1)
            {
                ActiveEngagementSlots.Add(claimedIndex);
                internalRequestTimer = .25f;
                return true;
            }

            return false;
        }

        public void ReleaseEngagementSlot(int index) {
            ActiveEngagementSlots.Remove(index);
        }

        public bool TryClaimAttackSlot(int engagementIndex) {

            if(ActiveAttackSlots.Count < context.maxAttackSlots) {
                ActiveAttackSlots.Add(engagementIndex);
                internalRequestTimer = .5f;
                return true;
            }

            return false;
        }

        public void ReleaseAttackSlot(int index) {
            ActiveAttackSlots.Remove(index);
        }

        public Vector3 GetSlotPosition(int index) {
            float arcAngle = context.slotArcAngle;
            int count = context.maxEngagementSlots;

            // what in the chat jippity is this TODO understand it
            // Evenly distribute inside the arc
            float step = count > 1 ? arcAngle / (count - 1) : 0f;

            // Center the arc around 0°
            float angle = -arcAngle * 0.5f + step * index;

            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * context.transform.forward;

            return context.transform.position + direction * context.slotRadius;
        }
    }
}
