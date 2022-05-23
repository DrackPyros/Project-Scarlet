using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour{
    void Start(){
        SelectFirstItem();
    }
    public void SelectFirstItem(){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(transform.Find("SelectLevelButton").gameObject);
    }
    public void Quit(){Application.Quit();}
    
}
