using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectorMenu : MonoBehaviour{
    // TODO: Obtener niveles, mostrarlos y cargar el seleccionado
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
    
    
    void Start(){ 
        _levelContainer = transform.Find("PictureContainer");
        _levelTemplate = _levelContainer.Find("LevelTemplate");

        _levelList = LoadLevelList();
        _levelTrasnformList = new List<Transform>();

        foreach(Level lvl in _levelList){
            CreateEntry(lvl, _levelContainer, _levelTrasnformList);
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
    }
    public List<Level> LoadLevelList(){
        List<Level> list = new List<Level>();
        Sprite img = null;
        // List<Sprite> imgList = new List<Image>(); // TODO: Crear e indexar imagenes
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++){
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
}
