using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private Rigidbody rb;
    private const int _runDelayFrames = 30;
    private const int _fallMultiplayer = 3;
    private const int _brakeDelay = 20;
    private const int _runSpeed = 550;
    private const int _walkSpeed = 400;
    public const int _jumpForce = 400;
    private int _speedUnitFrames = 0;
    public bool _onwalljump = false;
    public int _direction = 0;
    
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        Ongroud();
    }
    public void Accelerate(int _direction){ //TODO: Acelerar despues de caer es muy lento
        // print(rb.velocity.x);
        if (rb.velocity.x < 15 && rb.velocity.x > -15){
            if (Ongroud()){
                if (_speedUnitFrames <= _runDelayFrames){
                    Move(_walkSpeed, _direction);
                    _speedUnitFrames++;
                }
                else
                    Move(_runSpeed, _direction);
            }
            else
                Move(_walkSpeed, _direction);
        }
    }
    public void Decelerate(){
        if (Ongroud()){
            // print("pep");
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
    void Move(int speed, int _direction){
        // print(Vector3.right * _direction * speed * Time.deltaTime); //TODO: El movimiento en camara lenta no funciona -> poca fuerza por Time.deltatime
        rb.AddForce(Vector3.right * _direction * speed * Time.deltaTime, ForceMode.Impulse); 
    }
    public void Jump(){
        if (Ongroud()){
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (Onwall()) Walljump();
    }
    void Fall(){
        rb.AddForce(Vector3.up * (Physics.gravity.y * _fallMultiplayer), ForceMode.Force);
    }
    void Walljump(){ // TODO: Bug colisiones y mantener momento
        // print(rb.velocity.x);
        rb.AddForce(_walkSpeed * -_direction, _jumpForce, 0, ForceMode.Impulse);
        _onwalljump = true;
        Rotate(-_direction);
    }
    public void Rotate(int dir){
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
    public void Stop(){
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    bool Ongroud(){
        if (Physics.Raycast(transform.position + (Vector3.right / 2), Vector3.down, 0.5f) || Physics.Raycast(transform.position - (Vector3.right / 2), Vector3.down, 0.5f)){ // Retrasar un poco
            _onwalljump = false;
            // Debug.DrawLine(transform.position + (Vector3.right / 2), Vector3.down, Color.green, 0.5f);
            // Debug.DrawLine(transform.position - (Vector3.right / 2), Vector3.down, Color.red, 0.5f);
            return true;
        }
        else
            return false;
    }
    bool Onwall(){
        if (Physics.Raycast(transform.position, transform.right * _direction, 0.5f) && !Ongroud())
            return true;
        else
            return false;
    }
    public bool GetOnWalljump() {return _onwalljump;}
    public int GetSpeedUnitFrames() {return _speedUnitFrames;}
    public int GetRunDelayFrames() {return _runDelayFrames;}
}
