using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour{
    //Selector de Volumen y Resolución
    void OnEnable(){
        SelectFirstItem();
    }
    public void SelectFirstItem(){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(transform.Find("BackButton").gameObject);
    }
}
