using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int speedUnit = 0;
    private int runDelay = 37;
    private int brakeDelay = 25;
    private int runSpeed = 500;
    private int walkSpeed = 250;
    public float axis;

    void Update(){
        axis = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (!Input.GetKey(KeyCode.LeftShift))
        {           
            //Movement
            if (Input.GetAxisRaw("Horizontal") != 0){
                accelerate();
            } else 
                decelerate();
            
            //Jump
        }
    }
    void accelerate(){
        if (speedUnit <= runDelay){
            walk();
            speedUnit++;
        }
        else
            run();
    }
    void decelerate(){
        if (speedUnit == 38) speedUnit = brakeDelay;
        if (speedUnit > 0){
            speedUnit--;
        }
        else
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void walk(){
        gameObject.GetComponent<Rigidbody>().AddForce(Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime, 0, 0, ForceMode.Impulse);
    }
    void run(){
        gameObject.GetComponent<Rigidbody>().AddForce(Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime, 0, 0, ForceMode.Impulse);
    }
}
