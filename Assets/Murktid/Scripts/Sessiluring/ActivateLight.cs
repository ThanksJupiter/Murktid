using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    public Light light;

    public void Activate() {
        Debug.Log("Light is Activated");
            light.enabled = true;

    }
}
