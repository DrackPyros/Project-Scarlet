using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private Animator _animator;
    private GameObject _gameController;

    void Start()
    {
        _gameController = GameObject.Find("GameController");
        _animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Floor") || other.CompareTag("Coin")){
            try{_animator.SetBool("OnGround", true);}
            catch{}

        } else if (other.CompareTag("Player") && transform.position.y > 1 || other.CompareTag("Destroy")){ 
            _gameController.GetComponent<LineViewer>().deSelector();
            Destroy(gameObject);
        }
    }
    public bool ongroud(){
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            return true;
        else
            return false;
    }
}
