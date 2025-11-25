using UnityEngine;

public class DoorBehaviour : MonoBehaviour {
    public bool _isDoorOpen = false;
    Vector3 _doorClosedPos;
    Vector3 _doorOpenPos;
    public float _doorSpeed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        _doorClosedPos = transform.position;
        _doorOpenPos = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        if(_isDoorOpen) {
            OpenDoor();
        }
        else if(!_isDoorOpen) {
            ClosedDoor();
        }
    }

    void OpenDoor() {
        if(transform.position != _doorOpenPos) {
            transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
        }
    }

    void ClosedDoor() {
        if(transform.position != _doorClosedPos) {
            transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
        }
    }
}
