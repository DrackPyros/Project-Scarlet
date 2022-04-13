using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int speedUnitFrames = 0;
    private int runDelayFrames = 30;
    private int fallMultiplayer = 3;
    private int brakeDelay = 20;
    private int runSpeed = 550;
    private int walkSpeed = 400;
    private int jumpForce = 400;
    private float axis;
    private bool canjump = true;
    public int wallJumpDirection = 0;
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
            if (Input.GetAxisRaw("Horizontal") != 0) accelerate();
            else if (wallJumpDirection == 0) decelerate();
            
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && canjump) jump();
            else if (canjump == false && rb.velocity.y < 0) rb.AddForce(Vector3.up * (Physics.gravity.y * fallMultiplayer), ForceMode.Force);

            //Wall Jump
            
            if (Input.GetKeyDown(KeyCode.Space) && wallJumpDirection != 0 && !canjump){ walljump(); print(wallJumpDirection);}
            else if (canjump == false && rb.velocity.y < 0) rb.AddForce(Vector3.up * (Physics.gravity.y * fallMultiplayer), ForceMode.Force);
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void walljump(){
        rb.AddForce(walkSpeed * wallJumpDirection, jumpForce, 0, ForceMode.Impulse);
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Floor") || other.CompareTag("Coin")){
            canjump = true;
        }
    }
}
