using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingView : MonoBehaviour
{
    [SerializeField] private TextAsset _file;
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highscoreTrasnformList;
    private List<Entry> _highscoreEntryList;

    [System.Serializable]
    public class Entry{
        public string name;
        public string score;
    }
    [System.Serializable]
    public class Players{
        public Entry[] players;
    }
    public Players _highscoreRaw = new Players();
    void Start(){
        print(_file.text);
        _highscoreRaw = JsonUtility.FromJson<Players>(_file.text);
        _entryContainer = transform.Find("HighscoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");

        _highscoreEntryList = FillList(_highscoreRaw);
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
    public List<Entry> FillList(Players raw){   
        List<Entry> list = new List<Entry>();    
        foreach (Entry e in raw.players){
            // print(e.name+ " - "+ e.score);
            list.Add(e);
        }
        list.Sort(SortByScore);
        return list;
    }
    static int SortByScore(Entry p1, Entry p2){
        return p1.score.CompareTo(p2.score);
    }
}
