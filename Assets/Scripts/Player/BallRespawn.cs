using UnityEngine;

public class BallRespawn : MonoBehaviour{
    private GameObject _camera;

    void Start(){
        _camera = GameObject.Find("Main Camera");
    }
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Destroy")){
            transform.position = new Vector3(0, 1, 0);
            _camera.GetComponent<CameraStartPosition>().ResetCamera();
            // Destroy(gameObject);
        }
    }
}
