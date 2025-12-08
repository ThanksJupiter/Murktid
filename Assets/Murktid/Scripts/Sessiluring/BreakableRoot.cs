using UnityEngine;

public class BreakableRoot : MonoBehaviour
{
    public GameObject brokenVersionPrefab;

    public void Break() {

        Debug.Log("Root is Breaking");

        if(brokenVersionPrefab != null) {
            Instantiate(brokenVersionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
