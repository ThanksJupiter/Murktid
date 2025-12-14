using UnityEngine;

namespace Murktid {
    public class PlayerReference : MonoBehaviour {
        public PlayerContext context;
        public HealthDisplayReference healthDisplayReference;
        public AmmoDisplayReference ammoDisplayReference;

        private void OnDrawGizmos() {

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                context.transform.position,
                context.transform.position +
                Quaternion.Euler(0, -context.slotArcAngle * 0.5f, 0) *
                context.transform.forward * context.slotRadius
            );

            Gizmos.DrawLine(
                context.transform.position,
                context.transform.position +
                Quaternion.Euler(0, context.slotArcAngle * 0.5f, 0) *
                context.transform.forward * context.slotRadius
            );

            for (int i = 0; i < context.maxEngagementSlots; i++)
            {
                Vector3 pos = GetSlotPosition(i);

                if(Application.isPlaying) {
                    Gizmos.color = context.controller.attackerSlotSystem.ActiveEngagementSlots.Contains(i)
                        ? Color.red
                        : Color.green;
                }
                else {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawSphere(pos, 0.12f);
            }
        }

        // replace this with good method, can't use the one in slot system because it is null in editor
        private Vector3 GetSlotPosition(int index) {
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
