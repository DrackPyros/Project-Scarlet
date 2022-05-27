using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour{
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
            _player.GetComponent<PlayerMovement>().Stop();
            _cronometer.GetComponent<Timer>().Stop();
            _recordTime = _cronometer.GetComponent<Timer>().getSavedTime();
            Save(_recordTime);
            // print(_cronometer.GetComponent<Timer>().getSavedTime());
            exit = true;
        }
    }
    void Save(string time){
        Transform score;
        _ui.gameObject.SetActive(false);
        _saveScreen.gameObject.SetActive(true);
        score = _saveScreen.transform.Find("Score");
        score.GetComponent<TMPro.TextMeshProUGUI>().text = time;
        _saveScreen.transform.Find("InitialsTxt").GetComponent<TMPro.TMP_InputField>().ActivateInputField();
    }
    public void WriteInFile(string name){ //TODO: Input mal alineado
        // print(name);
        FileUser.Save(_recordTime, name, SceneManager.GetActiveScene().buildIndex);
    }
}
