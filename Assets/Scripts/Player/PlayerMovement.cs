using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int speedUnitFrames = 0;
    private int runDelayFrames = 30;
    private int jumpFrames = 40;
    private int jumpFallFrames = 10;
    private int brakeDelay = 25;
    private int runSpeed = 550;
    private int walkSpeed = 400;
    public int jumpForce = 1000;
    private float axis;
    private bool canjump = true;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        axis = Input.GetAxis("Horizontal");
    }
    void FixedUpdate(){
        if (!Input.GetKey(KeyCode.LeftShift))
        {           
            //Movement
            if (Input.GetAxisRaw("Horizontal") != 0){
                accelerate();
            } else 
                decelerate();
            
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && canjump) jump();
        }
    }
    void accelerate(){
        // move(runSpeed);
        if (speedUnitFrames <= runDelayFrames){
            move(walkSpeed);
            speedUnitFrames++;
        }
        else
            move(runSpeed);
    }
    void decelerate(){
        if (speedUnitFrames == (runDelayFrames + 1)) speedUnitFrames = brakeDelay;
        if (speedUnitFrames > 0){
            speedUnitFrames--;
        }
        else{
            Vector3 eraseX = rb.velocity;
            eraseX.x = 0;
            rb.velocity = eraseX;
        }
    }
    void move(int speed){
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime, ForceMode.Impulse);
    }
    void jump(){
        canjump = false;
        int i = 0;
        while (i < jumpFrames){
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse); //ascent
            i++;
            if (i == (jumpFrames - jumpFallFrames)){ //peak
                Vector3 eraseY = rb.velocity;
                eraseY.y = 0;
                rb.velocity = eraseY;
                print(i);
            } else if (i > (jumpFrames - jumpFallFrames)){ //fall
                rb.AddForce(Vector3.down * (jumpForce * 3) * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
    void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Floor") || other.CompareTag("Coin"))
        {
            canjump = true;
        }
    }
}
