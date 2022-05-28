using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private Rigidbody rb;
    private const int _runDelayFrames = 30;
    private const int _fallMultiplayer = 3;
    private const int _brakeDelay = 20;
    private const int _runSpeed = 550;
    private const int _walkSpeed = 400;
    public const int _jumpForce = 400;
    public float _raycastDistance = 0.001f;
    private int _speedUnitFrames = 0;
    public bool _onwalljump = false;
    public int _direction = 0;
    
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update(){
        Ongroud();
        Onwall();
    }
    public void Accelerate(int _direction){ //TODO: Acelerar despues de caer es muy lento
        if (rb.velocity.x < 15 && rb.velocity.x > -15){
            if(_onwalljump){ //Landing aceleration
                for(int i = 0; i< 2; i++)
                    Move(_runSpeed, _direction);
            }
            if (Ongroud()){
                if (_speedUnitFrames <= _runDelayFrames){
                    Move(_walkSpeed, _direction);
                    _speedUnitFrames++;
                }
                else
                    Move(_runSpeed, _direction);
            }
            else
                Move(_walkSpeed/2 , _direction);
        }
    }
    public void Decelerate(){
        if (Ongroud()){
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
        rb.AddForce(Vector3.right * _direction * speed * Time.deltaTime* 60, ForceMode.Impulse); 
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
        rb.AddForce((_walkSpeed * -_direction)* 1.5f, _jumpForce, 0, ForceMode.Impulse);
        _onwalljump = true;
        Rotate(-_direction);
    }
    public void Rotate(int dir){
        if (_direction != dir){
            if (_direction != 0){
                transform.Rotate(0, 180, 0, Space.Self);
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
        if (Physics.Raycast(transform.position + (Vector3.right / 2), Vector3.down, _raycastDistance) || Physics.Raycast(transform.position - (Vector3.right / 2), Vector3.down, 0.5f)){ // Retrasar un poco
            _onwalljump = false;
            Debug.DrawLine(transform.position + (Vector3.right / 2), Vector3.down, Color.green, _raycastDistance);
            Debug.DrawLine(transform.position - (Vector3.right / 2), Vector3.down, Color.red, _raycastDistance);
            return true;
        }
        else
            return false;
    }
    bool Onwall(){
            Debug.DrawLine(transform.position + Vector3.up, new Vector3((transform.position.x + 1)* _direction, transform.position.y + 1, transform.position.z), Color.blue, _raycastDistance);
        if (Physics.Raycast(transform.position, new Vector3((transform.position.x + 1)* _direction, transform.position.y + 1, transform.position.z),  _raycastDistance) && !Ongroud())
            return true;
        else
            return false;
    }
    public bool GetOnWalljump() {return _onwalljump;}
    public int GetSpeedUnitFrames() {return _speedUnitFrames;}
    public int GetRunDelayFrames() {return _runDelayFrames;}
}
