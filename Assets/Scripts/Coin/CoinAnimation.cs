using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private Animator a;
    private GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        a = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Floor") || other.CompareTag("Coin")){
            try{a.SetBool("OnGround", true);}
            catch{}

        } else if (other.CompareTag("Player") && transform.position.y > 1 || other.CompareTag("Destroy")){ // TODO: destruir moneda contacto player
            // print("pep");
            gameController.GetComponent<LineViewer>().deSelect(gameObject);
            Destroy(gameObject);
        }
        // print(other.tag);
    }
    public bool ongroud(){
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            return true;
        else
            return false;
    }
}
