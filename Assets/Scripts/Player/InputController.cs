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
    void Awake(){
        controls = new InputManager();
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController");

        controls.InGame.Time.performed += _ => timeSlow();
        controls.InGame.Time.canceled += _ => stopSlow();

        controls.InGame.Jump.performed += _ => player.GetComponent<PlayerMovement>().jump();

        controls.InGame.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
        controls.InGame.Move.canceled += _ => {x = 0; y = 0;};

        controls.InGame.CoinSelector.performed += contx => select(contx.ReadValue<float>());

        controls.InGame.Repel.performed += ctx => gameController.GetComponent<Magnetism>().push(-(ctx.ReadValue<float>()));
        controls.InGame.Attract.performed += ctx => gameController.GetComponent<Magnetism>().pull(ctx.ReadValue<float>());

        controls.InGame.Repel.canceled += _ => gameController.GetComponent<Magnetism>().nullForce(false);
        controls.InGame.Attract.canceled += _ => gameController.GetComponent<Magnetism>().nullForce(true);

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
                    move(value);
                else
                    player.GetComponent<PlayerMovement>().decelerate();
            }
        }
    }
    private void OnEnable(){controls.Enable();}
    private void OnDisable(){controls.Disable();}
    void move(Vector2 aux){ //TODO: revisar mover time
        if(!shootmode){
            value = aux;
            int val = (int)Mathf.Round(aux.x);
            if (!player.GetComponent<PlayerMovement>().getOnWalljump()){
                player.GetComponent<PlayerMovement>().accelerate(val);
                player.GetComponent<PlayerMovement>().rotate(val);
            }
        } else { 
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
        if (gameController.GetComponent<LineRenderer>().enabled == false){
            gameController.GetComponent<LineRenderer>().enabled = true;
        }
        gameController.GetComponent<Trajectory>().SimulateTraectory(new Vector3(transform.position.x + x, transform.position.y + y, 0));
    }
    void destroyTrayectory(){
        shootmode = false;
        gameController.GetComponent<LineRenderer>().enabled = false;;
    }
    public float getX(){ return x; }
    public float getY(){ return y; }
}
