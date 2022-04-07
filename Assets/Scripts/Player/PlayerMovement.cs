using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool maxSpeed = false;
    public float axis;
    void Update(){
        axis = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            //Movement
            if (Input.GetAxisRaw("Horizontal") != 0){
                if (!maxSpeed){
                    accelerate();
                }
            } else 
                decelerate();
            
            //Jump
        }
    }
    void accelerate(){
        int i = 0;
        if (i <= 4){
            gameObject.GetComponent<Rigidbody>().AddForce(Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0, ForceMode.Impulse);
            i++;
        }
        else
            maxSpeed = true;
    }
    void decelerate(){
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        maxSpeed = false;
    }
}
