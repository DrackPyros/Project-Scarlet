using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    private float x, y;
    private int force = 150;
    private GameObject player;

    void Start()
    {
        ignite();
    }
    public void ignite(){
        player = GameObject.Find("Player");
        x = player.GetComponent<InputController>().getX();
        y = player.GetComponent<InputController>().getY();
        x = x * force;
        y = y * force;
        gameObject.GetComponent<Rigidbody>().AddForce(x * Time.deltaTime, y * Time.deltaTime, 0, ForceMode.Impulse);
        // Destroy(gameObject, 5);
    
    }
}