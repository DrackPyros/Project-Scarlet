using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartPosition : MonoBehaviour{
    private Vector3 _startPosition;

    void Start(){
        if (gameObject.GetComponent<Camera>().aspect < 2){
            transform.position = new Vector3 (9, transform.position.y, transform.position.z);
        }
        else if(gameObject.GetComponent<Camera>().aspect > 3){
            transform.position = new Vector3 (17, transform.position.y, transform.position.z);
        }
        else if(gameObject.GetComponent<Camera>().aspect > 2.7f){
            transform.position = new Vector3 (15, transform.position.y, transform.position.z);
        }
        else if(gameObject.GetComponent<Camera>().aspect > 2.3f){
            transform.position = new Vector3 (14, transform.position.y, transform.position.z);
        }
        
        _startPosition = transform.position;
    }
    void Update(){
        // Debug.Log("Aspect Ratio : " + gameObject.GetComponent<Camera>().aspect); //TODO: comprobar resolución del proyector y hacer un media query
    }
    public void ResetCamera(){
        transform.position = _startPosition;
    }
}
