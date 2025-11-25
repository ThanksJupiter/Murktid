using System.Collections;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    [SerializeField] DoorBehaviour _doorBehaviour;

    [SerializeField] bool _isDoorOpenSwitch;
    [SerializeField] bool _isDoorClosedSwitch;


    float _switchSizeZ;
    Vector3 _switchInPos;
    Vector3 _switchOutPos;
    float _switchSpeed =1f;
    float _switchDelay = 0.2f;
    bool _isPressingSwitch = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _switchSizeZ = transform.localScale.z / 2;

        _switchInPos = transform.position;
        _switchOutPos = new Vector3(transform.position.x, transform.position.y - _switchSizeZ, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPressingSwitch) {
            MoveSwitchIn();
        }
        else if(!_isPressingSwitch) {
            MoveSwitchOut();
        }
    }

    void MoveSwitchIn() {
        if(transform.position != _switchInPos) {
            transform.position = Vector3.MoveTowards(transform.position, _switchInPos, _switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchOut() {
        if(transform.position != _switchOutPos) {
            transform.position = Vector3.MoveTowards(transform.position, _switchOutPos, _switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
        {
        if(collision.CompareTag("Player"))
            {
            _isPressingSwitch = !_isPressingSwitch;

            if(_isDoorOpenSwitch && !_doorBehaviour._isDoorOpen)
            {
                _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen;
            }
            else if(_isDoorClosedSwitch && _doorBehaviour._isDoorOpen) {
                _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen;
            }
        }
    }

    private void OnTriggerExit(Collider collision) {

        if(collision.CompareTag("Player")) {

            StartCoroutine(SwitchOutDelay(_switchDelay));
        }

        }
    IEnumerator SwitchOutDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        _isPressingSwitch = false;
    }
}

