using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public GameObject player;
    private bool canwalljump = false;
    private int direction = 0;
    void Update()
    {
        player.GetComponent<PlayerMovement>().wallJumpDirection = direction;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            canwalljump = true;
        }
    }
    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            canwalljump = false;
            direction = 0;
        }
    }

    void OnCollisionEnter(Collision collision){
        if (canwalljump){
            ContactPoint point = collision.contacts[0];
            print(point.point);
            direction = point.point.x > 0 ? -1 : 1;
        }
        // var contact : ContactPoint = collision.contacts[0];
        // contact.point; //this is the Vector3 position of the point of contact
    }
}
