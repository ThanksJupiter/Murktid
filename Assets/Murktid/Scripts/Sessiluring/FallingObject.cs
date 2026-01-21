using UnityEngine;

public class FallingObject : MonoBehaviour
{
   public Rigidbody rigidbody;

   public void Activate() {
       rigidbody.isKinematic = false;
   }


}
