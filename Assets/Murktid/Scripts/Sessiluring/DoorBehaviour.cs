using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

   /* [SerializeField] bool _isDoorOpen = false;*/

    [SerializeField] float _openAngle = 90f;
    [SerializeField] float _speed = 5f;

    bool _isOpen = false;
    Quaternion _closedRotation;     
    Quaternion _openedRotation;

    public bool IsOpen => _isOpen;

    void Awake() {       
        _closedRotation = transform.localRotation;
       
        _openedRotation = _closedRotation * Quaternion.Euler(0f, _openAngle, 0f);
    }

    public void Open() {
        if(_isOpen) return;
        _isOpen = true;
        StopAllCoroutines();
        StartCoroutine(RotateTo(_openedRotation));
    }

    public void Close() {
        if(!_isOpen) return;
        _isOpen = false;
        StopAllCoroutines();
        StartCoroutine(RotateTo(_closedRotation));
    }

    public void Toggle() {
        if(_isOpen) Close();
        else Open();
    }


    System.Collections.IEnumerator RotateTo(Quaternion targetRotation) {
        Quaternion start = transform.localRotation;
        float t = 0f;

                
        while(t < 1f) {
            t += Time.deltaTime *( _speed * 0.5f);
            transform.localRotation = Quaternion.Lerp(start, targetRotation, t);
            yield return null;
        }
        transform.localRotation = targetRotation;
        }  
}
