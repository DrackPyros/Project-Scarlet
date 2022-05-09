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
        if (other.CompareTag("Floor") || other.CompareTag("Coin"))
        {
            try{
                a.SetBool("OnGround", true);
            }
            catch{}
        }
    }
    public bool ongroud(){
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f)){
            return true;
        }
        else
            return false;
    }
}
