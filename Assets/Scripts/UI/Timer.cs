using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float _startTime;
    private bool _continue = true;
    private string _savedTime;
    private string _timerString;

    void Start () {
        _startTime = Time.time;
    }
    void Update(){
        float TimerControl = Time.time - _startTime;
        string mins = ((int)TimerControl/60).ToString("00");
        string segs = (TimerControl % 60).ToString("00");
        string milisegs = ((TimerControl * 100)%100).ToString ("00");
            
        _timerString = string.Format ("{00}:{01}:{02}", mins, segs, milisegs);
        if(_continue)
            GetComponent<Text>().text = _timerString.ToString();
    }

    public void Stop(){
        _continue = false;
        _savedTime = _timerString;
        // print(_savedTime);
    }
    public string getSavedTime(){
        return _savedTime;
    }
}
