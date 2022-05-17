using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputManager _controls;
    private GameObject _player, _gameController;
    private Vector2 _value;
    private bool _timezone = false;
    private bool _shootmode = false;
    private float _x = 0, _y = 0;
    void Awake(){
        _controls = new InputManager();
        _player = GameObject.Find("Player");
        _gameController = GameObject.Find("GameController");

        _controls.InGame.Time.performed += _ => timeSlow();
        _controls.InGame.Time.canceled += _ => stopSlow();

        _controls.InGame.Jump.performed += _ => _player.GetComponent<PlayerMovement>().jump();

        _controls.InGame.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
        _controls.InGame.Move.canceled += _ => {_x = 0; _y = 0;};

        _controls.InGame.CoinSelector.performed += contx => select(contx.ReadValue<float>());

        _controls.InGame.VialSelector.performed += contx => _gameController.GetComponent<MetalSelector>().VialSelector(contx.ReadValue<float>());

        _controls.InGame.Repel.performed += ctx => _gameController.GetComponent<Magnetism>().push(-(ctx.ReadValue<float>()));
        _controls.InGame.Attract.performed += ctx => _gameController.GetComponent<Magnetism>().pull(ctx.ReadValue<float>());
        _controls.InGame.Repel.canceled += _ => _gameController.GetComponent<Magnetism>().nullForce(false);
        _controls.InGame.Attract.canceled += _ => _gameController.GetComponent<Magnetism>().nullForce(true);

        _controls.InGame.ShootMode.performed += _ => _shootmode = true;
        _controls.InGame.ShootMode.canceled += _ => destroyTrayectory();

        _controls.InGame.Shoot.performed += _ => shoot();
        // _controls.InGame.Menu.performed += _ => _controls.SwitchCurrentActionMap("Menu");
    }
    void Update(){
        if(!_controls.InGame.Time.IsPressed()){
            _timezone = false;
            _gameController.GetComponent<LineViewer>().setWatch(false);
            if(!_shootmode){
                if(_controls.InGame.Move.IsPressed())
                    move(_value);
                else
                    _player.GetComponent<PlayerMovement>().decelerate();
            }
        }
    }
    private void OnEnable(){_controls.Enable();}
    private void OnDisable(){_controls.Disable();}
    void move(Vector2 aux){
        if(!_shootmode){
            _value = aux;
            int val = (int)Mathf.Round(aux.x);
            if (!_player.GetComponent<PlayerMovement>().getOnWalljump()){
                _player.GetComponent<PlayerMovement>().accelerate(val);
                _player.GetComponent<PlayerMovement>().rotate(val);
            }
        } else { 
            _x = aux.x;
            _y = aux.y;
            generateTrayectory(_x, _y);
        }
    }
    void timeSlow(){
        _timezone = true;
        _gameController.GetComponent<SlowMotion>().slowmo(true);
        _gameController.GetComponent<LineViewer>().setWatch(true);
        _gameController.GetComponent<LineViewer>().viewer();
    }
    void stopSlow(){
        _gameController.GetComponent<LineViewer>().setWatch(false);
        _gameController.GetComponent<LineViewer>().deSelector();
        _gameController.GetComponent<SlowMotion>().slowmo(false);
    }
    void select(float aux){
        if(_timezone){
            int val = (int)Mathf.Round(aux);
            _gameController.GetComponent<LineViewer>().coinChanger(val);
        }
    }
    void shoot(){
        if (_shootmode)
            _player.GetComponent<CoinThrowPosition>().coinPosition(_x, _y);
    }
    void generateTrayectory(float _x, float _y){
        if (_gameController.GetComponent<LineRenderer>().enabled == false){
            _gameController.GetComponent<LineRenderer>().enabled = true;
        }
        _gameController.GetComponent<Trajectory>().SimulateTraectory(new Vector3(transform.position.x + _x, transform.position.y + _y, 0));
    }
    void destroyTrayectory(){
        _shootmode = false;
        _gameController.GetComponent<LineRenderer>().enabled = false;;
    }
    public float getX(){ return _x; }
    public float getY(){ return _y; }
}
