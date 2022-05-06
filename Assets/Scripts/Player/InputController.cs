using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputManager controls;
    private GameObject player, gameController;
    private Vector2 value;
    private bool timezone = false;
    private bool shootmode = false;
    private float x = 0, y = 0;
    private Trajectory _projection;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Material invisibleMaterial, lineMaterial;
    void Awake(){
        controls = new InputManager();
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController");
        _projection = gameController.GetComponent<Trajectory>();

        controls.InGame.Time.performed += _ => timeSlow();
        controls.InGame.Time.canceled += _ => stopSlow();

        controls.InGame.Jump.performed += _ => player.GetComponent<PlayerMovement>().jump();

        controls.InGame.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
        controls.InGame.Move.canceled += ctx => {x = 0; y = 0;};

        controls.InGame.Selector.performed += contx => select(contx.ReadValue<float>());

        controls.InGame.Repel.performed += ctx => gameController.GetComponent<Magnetism>().push(-(ctx.ReadValue<float>()));
        controls.InGame.Attract.performed += ctx => gameController.GetComponent<Magnetism>().pull(ctx.ReadValue<float>());

        controls.InGame.ShootMode.performed += _ => shootmode = true;
        controls.InGame.ShootMode.canceled += _ => destroyTrayectory();

        controls.InGame.Shoot.performed += _ => shoot();
    }
    void Update(){
        if(!controls.InGame.Time.IsPressed()){
            timezone = false;
            gameController.GetComponent<LineViewer>().setWatch(false);
            if(!shootmode){
                if(controls.InGame.Move.IsPressed())
                    move(value); //Check air movement
                else
                    player.GetComponent<PlayerMovement>().decelerate();
            }
        }
    }
    private void OnEnable(){controls.Enable();}
    private void OnDisable(){controls.Disable();}
    void move(Vector2 aux){ // limitar movimiento aereo
        // print (aux);
        if(!timezone && !shootmode){
            value = aux;
            int val = (int)Mathf.Round(aux.x);
            if (!player.GetComponent<PlayerMovement>().getOnWalljump()){
                player.GetComponent<PlayerMovement>().accelerate(val);
                player.GetComponent<PlayerMovement>().rotate(val);
            }
        } else if (shootmode){ 
            x = aux.x;
            y = aux.y;
            generateTrayectory(x, y);
        }
    }
    void timeSlow(){
        timezone = true;
        gameController.GetComponent<SlowMotion>().slowmo(true);
        gameController.GetComponent<LineViewer>().setWatch(true);
        gameController.GetComponent<LineViewer>().viewer();
    }
    void stopSlow(){
        gameController.GetComponent<LineViewer>().setWatch(false);
        gameController.GetComponent<LineViewer>().deSelector();
        gameController.GetComponent<SlowMotion>().slowmo(false);
    }
    void select(float aux){
        if(timezone){
            int val = (int)Mathf.Round(aux);
            gameController.GetComponent<LineViewer>().coinChanger(val);
        }
    }
    void shoot(){
        if (shootmode)
            player.GetComponent<CoinThrowPosition>().coinPosition(x, y);
    }
    void generateTrayectory(float x, float y){
        if (gameController.GetComponent<LineRenderer>().material.name != lineMaterial.name){
            // print(gameController.GetComponent<LineRenderer>().material.name);
            gameController.GetComponent<LineRenderer>().material = lineMaterial;
        }
        _projection.SimulateTraectory(_coin, new Vector3(transform.position.x + x, transform.position.y + y, 0));
    }
    void destroyTrayectory(){
        shootmode = false;
        gameController.GetComponent<LineRenderer>().material = invisibleMaterial;
    }
    public float getX(){ return x; }
    public float getY(){ return y; }
}
