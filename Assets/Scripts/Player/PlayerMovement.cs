using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int _speedUnitFrames = 0;
    private int _runDelayFrames = 30;
    private int _fallMultiplayer = 3;
    private int _brakeDelay = 20;
    private int _runSpeed = 550;
    private int _walkSpeed = 400;
    public int _jumpForce = 400;
    public bool _onwalljump = false;
    public int _direction = 0;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        ongroud();
    }
    public void accelerate(int _direction){ //TODO: revisar landing accelerate
        // print(rb.velocity.x);
        if (rb.velocity.x < 15 && rb.velocity.x > -15){
            if (ongroud()){
                if (_speedUnitFrames <= _runDelayFrames){
                    move(_walkSpeed, _direction);
                    _speedUnitFrames++;
                }
                else
                    move(_runSpeed, _direction);
            }
            else
                move(_walkSpeed, _direction);
        }
    }
    public void decelerate(){
        if (ongroud()){
            if (_speedUnitFrames == (_runDelayFrames + 1)) _speedUnitFrames = _brakeDelay;
            if (_speedUnitFrames > 0){
                _speedUnitFrames--;
            }
            else{
                Vector3 eraseX = rb.velocity;
                eraseX.x = 0;
                rb.velocity = eraseX;
            }
        }
    }
    void move(int speed, int _direction){
        // print("entra");
        rb.AddForce(Vector3.right * _direction * speed * Time.deltaTime, ForceMode.Impulse);
    }
    public void jump(){
        if (ongroud()){
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (onwall()) walljump();
    }
    void fall(){
        rb.AddForce(Vector3.up * (Physics.gravity.y * _fallMultiplayer), ForceMode.Force);
    }
    void walljump(){ // TODO: Bug colisiones y mantener momento
        // print(rb.velocity.x);
        rb.AddForce(_walkSpeed * -_direction, _jumpForce, 0, ForceMode.Impulse);
        _onwalljump = true;
        rotate(-_direction);
    }
    public void rotate(int dir){
        if (_direction != dir){
            if (_direction != 0){
                transform.Rotate(180, 0, 0, Space.Self);
                _direction = -_direction;
            }
            else
                _direction = dir;
        }
        else if (_onwalljump)
            _direction = -_direction; 
    }
    bool ongroud(){
        if (Physics.Raycast(transform.position + (Vector3.right / 2), Vector3.down, 0.5f) || Physics.Raycast(transform.position - (Vector3.right / 2), Vector3.down, 0.5f)){ // Retrasar un poco
            _onwalljump = false;
            // Debug.DrawLine(transform.position + (Vector3.right / 2), Vector3.down, Color.green, 0.5f);
            // Debug.DrawLine(transform.position - (Vector3.right / 2), Vector3.down, Color.red, 0.5f);
            return true;
        }
        else
            return false;
    }
    bool onwall(){
        if (Physics.Raycast(transform.position, transform.right * _direction, 0.5f) && !ongroud())
            return true;
        else
            return false;
    }
    public bool getOnWalljump() {return _onwalljump;}
    public int getSpeedUnitFrames() {return _speedUnitFrames;}
    public int getRunDelayFrames() {return _runDelayFrames;}
}
