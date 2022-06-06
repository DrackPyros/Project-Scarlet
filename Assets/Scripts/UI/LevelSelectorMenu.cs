using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class LevelSelectorMenu : MonoBehaviour{
    public class Level{
        public string title;
        public Sprite img;
        public int id;
        public Level(string title, Sprite img, int id){
            this.title = title;
            this.img = img;
            this.id = id;
        }
    }
    private Transform _levelContainer;
    private Transform _levelTemplate;
    private List<Transform> _levelTrasnformList;
    private List<Level> _levelList;
    private bool _selected = false;
    public List<Texture2D> imgList; //Añadir textura en el inspector
    
    
    void Start(){ 
        _levelContainer = transform.Find("PictureContainer");
        _levelTemplate = _levelContainer.Find("LevelTemplate");

        _levelList = LoadLevelList();
        _levelTrasnformList = new List<Transform>();

        foreach(Level lvl in _levelList){
            CreateEntry(lvl, _levelContainer, _levelTrasnformList);
        }
    }
    void OnEnable(){
        if (_selected){
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(transform.Find("BackButton").gameObject);
        }
    }
    void CreateEntry(Level lvl, Transform container, List<Transform> transformList){
        float padding = 100f;
        Transform entryTransform = Instantiate(_levelTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(+padding * transformList.Count, 0);
        entryTransform.gameObject.SetActive(true);
    
        entryTransform.Find("LevelImage").GetComponent<Image>().sprite = lvl.img;
        entryTransform.Find("LevelTitle").GetComponent<TMPro.TextMeshProUGUI>().text = lvl.title;
        entryTransform.Find("SelectLevel").GetComponent<Button>().onClick.AddListener(() => LoadScene(lvl.id));
        transformList.Add(entryTransform);
        if (!_selected){
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(entryTransform.GetChild(2).gameObject);
            _selected = !_selected;
        }
    }
    public List<Level> LoadLevelList(){
        List<Level> list = new List<Level>();
        Sprite img = null;
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++){
            img = Sprite.Create(imgList[i-1], new Rect(0, 0, imgList[i-1].width, imgList[i-1].height), new Vector2(0.5f, 0.5f));
            string title = SceneUtility.GetScenePathByBuildIndex(i);
            title = title.Substring(14, 6);
            Level aux = new Level(title, img, i);
            list.Add(aux);
        }
        return list;
    }
    public void LoadScene(int id){
        SceneManager.LoadScene(id);
    }
    // void OnDisable(){_selected = false;}
}
