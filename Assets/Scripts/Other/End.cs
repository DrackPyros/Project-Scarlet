using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private GameObject _cronometer;
    void Start(){
        _cronometer = GameObject.Find("Cronometer");
    }
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Player")){
            Time.timeScale = 0;
            _cronometer.GetComponent<Timer>().Stop();
        }
    }
}
