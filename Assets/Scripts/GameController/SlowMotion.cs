using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Slowmotion
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            //Stop slowmotion
        }
    }
}
