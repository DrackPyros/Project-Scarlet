using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour{
    private InputManager _controls;
    private GameObject _player, _gameController;
    public GameObject _menu;
    private Vector2 _value;
    private bool _timezone = false;
    private bool _shootmode = false;
    private float _x = 0, _y = 0;
    
    void Awake(){
        _controls = new InputManager();
        _player = GameObject.Find("Player");
        _gameController = GameObject.Find("GameController");

        _controls.InGame.Time.performed += _ => TimeSlow();
        _controls.InGame.Time.canceled += _ => StopSlow();

        _controls.InGame.Jump.performed += _ => _player.GetComponent<PlayerMovement>().Jump();

        _controls.InGame.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        _controls.InGame.Move.canceled += _ => {_x = 0; _y = 0;};

        _controls.InGame.CoinSelector.performed += contx => Select(contx.ReadValue<float>());

        _controls.InGame.VialSelector.performed += contx => _gameController.GetComponent<MetalSelector>().VialSelector(contx.ReadValue<float>());

        _controls.InGame.Repel.performed += ctx => _gameController.GetComponent<Magnetism>().Push(-(ctx.ReadValue<float>()));
        _controls.InGame.Attract.performed += ctx => _gameController.GetComponent<Magnetism>().Pull(ctx.ReadValue<float>());
        _controls.InGame.Repel.canceled += _ => _gameController.GetComponent<Magnetism>().NullForce(false);
        _controls.InGame.Attract.canceled += _ => _gameController.GetComponent<Magnetism>().NullForce(true);

        _controls.InGame.ShootMode.performed += _ => _shootmode = true;
        _controls.InGame.ShootMode.canceled += _ => DestroyTrayectory();

        _controls.InGame.Shoot.performed += _ => Shoot();
        _controls.InGame.Menu.performed += _ => Menu();

        // _controls.Menu.Back.performed += _ => BackButton();
    }
    void Update(){
        if(!_controls.InGame.Time.IsPressed()){
            _timezone = false;
            _gameController.GetComponent<LineViewer>().SetWatch(false);
            if(!_shootmode){
                if(_controls.InGame.Move.IsPressed())
                    Move(_value);
                else
                    _player.GetComponent<PlayerMovement>().Decelerate();
            }
        }
    }
    private void OnEnable(){_controls.Enable();}
    private void OnDisable(){_controls.Disable();}
    void Move(Vector2 aux){
        if(!_shootmode){
            _value = aux;
            int val = (int)Mathf.Round(aux.x);
            if (!_player.GetComponent<PlayerMovement>().GetOnWalljump()){
                _player.GetComponent<PlayerMovement>().Accelerate(val);
                _player.GetComponent<PlayerMovement>().Rotate(val);
            }
        } else { 
            _x = aux.x;
            _y = aux.y;
            GenerateTrayectory(_x, _y);
        }
    }
    void TimeSlow(){
        _timezone = true;
        SlowMotion.Slowmo(true);
        _gameController.GetComponent<LineViewer>().SetWatch(true);
        _gameController.GetComponent<LineViewer>().Viewer();
    }
    void StopSlow(){
        _gameController.GetComponent<LineViewer>().SetWatch(false);
        _gameController.GetComponent<LineViewer>().DeSelector();
        SlowMotion.Slowmo(false);
    }
    void Select(float aux){
        if(_timezone){
            int val = (int)Mathf.Round(aux);
            _gameController.GetComponent<LineViewer>().CoinChanger(val);
        }
    }
    void Shoot(){
        if (_shootmode)
            _player.GetComponent<CoinThrowPosition>().CoinPosition(_x, _y);
    }
    void GenerateTrayectory(float _x, float _y){
        if (_gameController.GetComponent<LineRenderer>().enabled == false){
            _gameController.GetComponent<LineRenderer>().enabled = true;
        }
        _gameController.GetComponent<Trajectory>().SimulateTraectory(new Vector3(transform.position.x + _x, transform.position.y + _y, 0));
    }
    void DestroyTrayectory(){
        _shootmode = false;
        _gameController.GetComponent<LineRenderer>().enabled = false;;
    }
    void Menu(){
        _menu.SetActive(true);
        ChangeControls(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_menu.transform.Find("Home").gameObject);
    }
    void ChangeControls(bool menu){
        if (menu){
            _controls.InGame.Disable();
            _controls.Menu.Enable();
        }
        else{
            _controls.Menu.Disable();
            _controls.InGame.Enable();
        }
    }
    void BackButton(){
        // transform.Find("BackButton").GetComponent<Button>().Invoke();
    }
    public float getX(){ return _x; }
    public float getY(){ return _y; }
}
