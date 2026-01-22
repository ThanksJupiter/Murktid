using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActivateLight : MonoBehaviour
{
    public Light light;
    public ParticleSystem particleSystem;

    private float timer = 1f;
    private float lerpSpeed = 1f;
    private float intensityTarget = 30f;

    public void Activate() {
        //Debug.Log("Light is Activated");
            light.enabled = true;
            if(particleSystem != null) {
                particleSystem.Play();
            }

    }

    private void Update() {
        if(light.enabled) {
            light.intensity = Mathf.Lerp(light.intensity, intensityTarget, 1f - Mathf.Exp(-lerpSpeed * Time.deltaTime));
            timer -= Time.deltaTime;

            if(timer <= 0f) {
                timer = Random.Range(.1f, .25f);
                lerpSpeed = Random.Range(3f, 7f);
                light.intensity = Random.Range(15f, 35f);
            }
        }
    }
}
