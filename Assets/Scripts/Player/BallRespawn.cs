using UnityEngine;

public class BallRespawn : MonoBehaviour{
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Destroy")){
            transform.position = new Vector3(0, 1, 0);
            // Destroy(gameObject);
        }
    }
}
