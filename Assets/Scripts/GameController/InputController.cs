using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputManager controls;
    private GameObject player, GameController;
    void Awake(){
        player = GameObject.Find("Player");
        GameController = GameObject.Find("GameController");
        controls.InputMaster.Jump.performed += _ => player.GetComponent<PlayerMovement>().jump();
        controls.InputMaster.Move.performed += ctx => move(ctx.ReadValue<int>());
    }
    void move(int value){
        print(value);
        // player.GetComponent<PlayerMovement>().accelerate();
        // player.GetComponent<PlayerMovement>().rotate();
    }
}
