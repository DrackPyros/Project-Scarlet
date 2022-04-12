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
        // print(other.tag);
        
        if (other.CompareTag("Floor") || other.CompareTag("Coin"))
        {
            a.SetBool("OnGround", true);
        }
    }
}
