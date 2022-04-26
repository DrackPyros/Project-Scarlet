using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polarity : MonoBehaviour
{
    public bool activar = false;
    public int atraer = -1; // Si -1 inversa de push
    public GameObject p1, p2;
    private Rigidbody rb1, rb2;
    private float efectArea;

    void Start(){
        rb1 = p1.GetComponent<Rigidbody>();
        rb2 = p2.GetComponent<Rigidbody>();
    }
    void Update(){
        efectArea = Vector3.Distance(p1.transform.position, p2.transform.position);
        if(Input.GetKey(KeyCode.Space)){
            activar = true;
            push();
        }
        // if (activar)
    }
    void push(){
        if (efectArea <= 20){
            p1.transform.position = Vector3.MoveTowards(p1.transform.position, p2.transform.position, atraer * rb2.mass * Time.deltaTime);
            p2.transform.position = Vector3.MoveTowards(p2.transform.position, p1.transform.position, atraer * rb1.mass * Time.deltaTime);
            // print(Vector3.Distance(p1.transform.position, p2.transform.position));

        }
    }
}
