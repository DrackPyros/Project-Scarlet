using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RankingView : MonoBehaviour{
    
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highscoreTrasnformList;
    private List<Entry> _highscoreEntryList;
    
    
    void Start(){
        ChangePage(1);
    }
    void OnEnable(){
        SelectFirstItem();
    }
    void CreateEntry(Entry entry, Transform container, List<Transform> transformList){
        float altura = 22f;
        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -altura * transformList.Count);
        entryTransform.gameObject.SetActive(true);
    
        entryTransform.Find("InitialsTxt").GetComponent<TMPro.TextMeshProUGUI>().text = entry.name;
        entryTransform.Find("ScoreTxt").GetComponent<TMPro.TextMeshProUGUI>().text = entry.score;
        transformList.Add(entryTransform);
    }
    public void SelectFirstItem(){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(transform.Find("BackButton").gameObject);
    }
    public void ChangePage(int page){
        _entryContainer = transform.Find("HighscoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");
        DeleteContent();

        _highscoreEntryList = FileUser.Load(page);
        _highscoreTrasnformList = new List<Transform>();

        foreach(Entry entry in _highscoreEntryList){
            CreateEntry(entry, _entryContainer, _highscoreTrasnformList);
        }
    }
    void DeleteContent(){
        GameObject[] transformList = GameObject.FindGameObjectsWithTag("Template");
        for(int i = 0; i < transformList.Length; i++){
            Destroy(transformList[i]);
        }
    }
}
