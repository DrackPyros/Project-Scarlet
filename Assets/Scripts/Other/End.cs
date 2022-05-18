using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private GameObject _cronometer, _player, _ui;
    public GameObject _saveScreen;
    private string _recordTime;
    private bool exit = false;
    void Start(){
        _cronometer = GameObject.Find("Cronometer");
        _player = GameObject.Find("Player");
        _ui = GameObject.Find("UI");
    }
    void OnTriggerEnter(Collider other){        
        if (other.CompareTag("Player") && !exit){
            _player.GetComponent<PlayerMovement>().stop();
            _cronometer.GetComponent<Timer>().Stop();
            _recordTime = _cronometer.GetComponent<Timer>().getSavedTime();
            save(_recordTime);
            // print(_cronometer.GetComponent<Timer>().getSavedTime());
            exit = true;
        }
    }
    void save(string time){
        Transform score;
        _ui.gameObject.SetActive(false);
        _saveScreen.gameObject.SetActive(true);
        score = _saveScreen.transform.Find("Score");
        score.GetComponent<TMPro.TextMeshProUGUI>().text = time;
        _saveScreen.transform.Find("InitialsTxt").GetComponent<TMPro.TMP_InputField>().ActivateInputField();
    }
    public void WriteInFile(string name){ //TODO: Input mal alineado & Escribir en el archivo
        print(_recordTime + " - " + name);
    }
}
