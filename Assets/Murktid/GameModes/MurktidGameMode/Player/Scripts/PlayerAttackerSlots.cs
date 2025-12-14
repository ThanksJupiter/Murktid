using System;
using UnityEngine;

namespace Murktid {

    public class PlayerAttackerSlots : MonoBehaviour {

        public PlayerAttackerSlot[] slots;

        public float slotDistanceFromPlayer = 2f;

        private Vector3[] directions = new[] {
            Vector3.forward, Vector3.right, Vector3.back, Vector3.left
        };

        private void OnValidate() {
            for(int i = 0; i < slots.Length; i++) {
                slots[i].transform.localPosition = directions[i] * slotDistanceFromPlayer;
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, slotDistanceFromPlayer);

            Gizmos.color = Color.blue;
            foreach(PlayerAttackerSlot slot in slots) {
                Gizmos.DrawSphere(slot.transform.position, .25f);
            }
        }
    }
}
