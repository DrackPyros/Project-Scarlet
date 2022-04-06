using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private Animator a;

    void Start()
    {
        a = GetComponent<Animator>();
    }
     void OnTriggerEnter(Collider other){
        print(other);
        
        if (other.CompareTag("Floor"))
        {
            a.SetBool("OnGround", true);
        }
    }
}
