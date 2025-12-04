using UnityEngine;

public class BreakableWall : MonoBehaviour {
    public GameObject brokenVersionPrefab;

    public void Break() {

        Debug.Log("Wall is Breaking");

        if(brokenVersionPrefab != null) {
            Instantiate(brokenVersionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
