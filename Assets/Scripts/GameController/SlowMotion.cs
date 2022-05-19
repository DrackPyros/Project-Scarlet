using UnityEngine;

public static class SlowMotion{
    public static void Slowmo(bool input){
        if(input){
            Time.timeScale = 0.5f;
        } else
            Time.timeScale = 1;
    }
}
