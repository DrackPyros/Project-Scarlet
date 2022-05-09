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
    public int jumpForce = 400;
    public bool onwalljump = false;
    public int direction = 0;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        // if (canjump == false && rb.velocity.y < 0) fall();
        ongroud();
        onwall();
    }
    public void accelerate(int direction){
        if (ongroud()){
            if (speedUnitFrames <= runDelayFrames){
                move(walkSpeed, direction);
                speedUnitFrames++;
            }
            else
                move(runSpeed, direction);
        }
        else
            move(walkSpeed, direction);
    }
    public void decelerate(){
        if (transform.position.y > 1 && ongroud()){
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
    }
    void move(int speed, int direction){
        rb.AddForce(Vector3.right * direction * speed * Time.deltaTime, ForceMode.Impulse);
    }
    public void jump(){
        if (ongroud()){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (onwall()) walljump();
    }
    void fall(){
        rb.AddForce(Vector3.up * (Physics.gravity.y * fallMultiplayer), ForceMode.Force);
    }
    void walljump(){
        rb.AddForce(walkSpeed * -direction, jumpForce, 0, ForceMode.Impulse);
        // canwalljump = false;
        onwalljump = true;
        rotate(-direction);
    }
    public void rotate(int dir){
        if (direction != dir){
            if (direction != 0){
                transform.Rotate(180, 0, 0, Space.Self);
                direction = -direction;
            }
            else
                direction = dir;
        }
        else if (onwalljump)
            direction = -direction; 
    }
    bool ongroud(){
        if (Physics.Raycast(transform.position + (Vector3.right / 2), Vector3.down, 0.5f) || Physics.Raycast(transform.position - (Vector3.right / 2), Vector3.down, 0.5f)){ // Retrasar un poco
            onwalljump = false;
            // Debug.DrawLine(transform.position + (Vector3.right / 2), Vector3.down, Color.green, 0.5f);
            // Debug.DrawLine(transform.position - (Vector3.right / 2), Vector3.down, Color.red, 0.5f);
            return true;
        }
        else
            return false;
    }
    bool onwall(){
        if (Physics.Raycast(transform.position, transform.right * direction, 0.5f) && !ongroud()){
            return true;
        }
        else
            return false;
    }
    void OnTriggerEnter(Collider other){
        // if (other.CompareTag("Floor") || other.CompareTag("Coin")){}
        // if (other.CompareTag("Wall")){ }// bug tiempo de caida al suelo
    }
    public bool getOnWalljump() {return onwalljump;}
}
