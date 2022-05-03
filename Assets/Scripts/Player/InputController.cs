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

        controls.InputMaster.Time.performed += _ => timeSlow();
        controls.InputMaster.Time.canceled += _ => gameController.GetComponent<LineViewer>().deSelector();
        controls.InputMaster.Jump.performed += _ => player.GetComponent<PlayerMovement>().jump();
        controls.InputMaster.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
        controls.InputMaster.Selector.performed += contx => select(contx.ReadValue<float>());
        // controls.InputMaster.Selector.canceled += _ => gameController.GetComponent<LineViewer>().deSelector();
        controls.InputMaster.ShootMode.performed += _ => shootmode = true;
        controls.InputMaster.Shoot.performed += _ => shoot();
        controls.InputMaster.Move.canceled += ctx => {x = 0; y = 0;};
    }
    void Update(){
        if(!controls.InputMaster.Time.IsPressed()){
            timezone = false;
            gameController.GetComponent<LineViewer>().setWatch(false);
            if(!shootmode){
                if(controls.InputMaster.Move.IsPressed())
                    move(value); //Check air movement
                else
                    player.GetComponent<PlayerMovement>().decelerate();
            }
        }
    }
    private void OnEnable(){
        controls.Enable();
    }
    private void OnDisable(){
        controls.Disable();
    }
    void move(Vector2 aux){
        print (aux);
        if(!timezone && !shootmode){
            value = aux;
            int val = (int)Mathf.Round(aux.x);
            if (player.GetComponent<PlayerMovement>().getCanjump()){
                player.GetComponent<PlayerMovement>().accelerate(val);
                player.GetComponent<PlayerMovement>().rotate(val);
            }
        } else if (shootmode){
            // print(aux.x + " a " + aux.y);
            x = aux.x;
            y = aux.y;
        }
    }
    void timeSlow(){
        timezone = true;
        gameController.GetComponent<SlowMotion>().slowmo();
        gameController.GetComponent<LineViewer>().setWatch(true);
        gameController.GetComponent<LineViewer>().viewer();
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
}
