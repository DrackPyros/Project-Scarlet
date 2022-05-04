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
        controls.InGame.Move.canceled += ctx => {x = 0; y = 0;};

        controls.InGame.Selector.performed += contx => select(contx.ReadValue<float>());

        controls.InGame.Repel.performed += ctx => gameController.GetComponent<Magnetism>().push(-(ctx.ReadValue<float>()));
        controls.InGame.Attract.performed += ctx => gameController.GetComponent<Magnetism>().pull(ctx.ReadValue<float>());

        controls.InGame.ShootMode.performed += _ => shootmode = true;
        controls.InGame.ShootMode.canceled += _ => shootmode = false;

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
    void move(Vector2 aux){
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
        }
    }
    void timeSlow(){
        timezone = true;
        gameController.GetComponent<SlowMotion>().slowmo(true);
        gameController.GetComponent<LineViewer>().setWatch(true);
        gameController.GetComponent<LineViewer>().viewer();
    }
    void stopSlow(){
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
    public float getX(){ return x; }
    public float getY(){ return y; }
}
