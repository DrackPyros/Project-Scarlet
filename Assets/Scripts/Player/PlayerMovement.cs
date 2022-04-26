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
    public bool canjump = true;
    public bool canwalljump = false;
    public bool onwalljump = false;
    public float direction = 0;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        if (!Input.GetKey(KeyCode.LeftShift))
        {           
            //Movement
            if (transform.position.y == 1){
                if (Input.GetAxisRaw("Horizontal") != 0) {accelerate(); rotate();}
                else if (transform.position.y > 1) decelerate();
            }
            
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && canjump) jump();
            else if (canjump == false && rb.velocity.y < 0) fall();

            //Wall Jump
            if (Input.GetKeyDown(KeyCode.Space) && canwalljump && !canjump){walljump();}
            else if (!canjump && rb.velocity.y < 0) fall();
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
    void fall(){
        rb.AddForce(Vector3.up * (Physics.gravity.y * fallMultiplayer), ForceMode.Force);
    }
    void walljump(){ //Bug salto vertical -> hacer collides mas pequeños
        rb.AddForce(walkSpeed * -direction, jumpForce, 0, ForceMode.Impulse);
        canwalljump = false;
        onwalljump = true;
        rotate();
    }
    void rotate(){
        if (direction != Input.GetAxisRaw("Horizontal")){
            if (direction != 0){
                transform.Rotate(180, 0, 0, Space.Self);
                direction = -direction;
            }
            else
                direction = Input.GetAxisRaw("Horizontal");
        }
        else if (onwalljump)
            direction = -direction;
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Floor") || other.CompareTag("Coin")){
            canjump = true;
            canwalljump = false;
            onwalljump = false;
        }
        if (other.CompareTag("Wall")){
            if (transform.position.y > 1){
                canwalljump = true;
            }
        }  
    }
}
