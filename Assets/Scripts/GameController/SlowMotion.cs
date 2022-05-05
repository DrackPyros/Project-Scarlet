using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public void slowmo(bool input)
    {
        if(input){
            Time.timeScale = 0.5f;
            // print("Sloooooow");
        } else
            Time.timeScale = 1;
    }
}
