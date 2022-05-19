using System.Collections.Generic;
using UnityEngine;

public class RankingView : MonoBehaviour{
    
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highscoreTrasnformList;
    private List<Entry> _highscoreEntryList;
    
    
    void Start(){ 
        _entryContainer = transform.Find("HighscoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");

        _highscoreEntryList = FileUser.Load();
        _highscoreTrasnformList = new List<Transform>();

        foreach(Entry entry in _highscoreEntryList){
            CreateEntry(entry, _entryContainer, _highscoreTrasnformList);
        }
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
}
