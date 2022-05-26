using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour{
    bool back = false;
    float distance = 2;
    void Update(){
        if(!back)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 9, transform.position.z), distance * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -5, transform.position.z), distance * Time.deltaTime);

        if(transform.position.y >= 8)
            back = true;

        if(transform.position.y <= -4)
            back = false;
    }
}
