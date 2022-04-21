using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polarity : MonoBehaviour
{
    public bool empujar = true;
    public GameObject p1, p2;
    private Rigidbody rb1, rb2;
    public float speed;

    void Start(){
        rb1 = p1.GetComponent<Rigidbody>();
        rb2 = p2.GetComponent<Rigidbody>();
    }
    void Update(){
        if (empujar)
            push(speed);
    }
    void push(float speed){
        p1.transform.position = Vector3.MoveTowards(p1.transform.position, p2.transform.position, speed * Time.deltaTime);
        p2.transform.position = Vector3.MoveTowards(p2.transform.position, p1.transform.position, speed * Time.deltaTime);
    }
}
