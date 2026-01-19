using System.Collections.Generic;
using UnityEngine;

namespace Murktid {

    public class DoorOpener : MonoBehaviour, ITarget, IInteractable {

        [SerializeField] List<DoorBehaviour> _doors = new List<DoorBehaviour>();

        [SerializeField] bool _isDoorOpenSwitch;
        [SerializeField] bool _isDoorClosedSwitch;


        public void Hit(float damage) {
            //Debug.Log($"Hit: { gameObject.name } for { damage }");
        }
        public void Stagger() {

        }

        public void Interact() {
            Debug.Log($"Interacted with: {gameObject.name}");

            foreach(var door in _doors) {
                if(door == null) continue;

                if(_isDoorOpenSwitch && !door.IsOpen) {
                    door.Open();
                }
                else if(_isDoorOpenSwitch && door.IsOpen) {
                    door.Close();
                }
                else if(!_isDoorOpenSwitch && !_isDoorClosedSwitch) {
                    door.Toggle();
                }


            }
        }
    }
}

