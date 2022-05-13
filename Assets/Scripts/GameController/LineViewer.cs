using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineViewer : MonoBehaviour
{
    [SerializeField] private Material _cyan;
    [SerializeField] private GameObject _player;
    private GameObject _selected = null;
    private GameObject[] _metal = new GameObject[]{};
    private bool _watch = false; 
    
    void Update(){
        if (_watch)
            viewer();
    }
    public void setWatch(bool value){
        _watch = value;
    }
    public void viewer(){
        if (_metal.Length < GameObject.FindGameObjectsWithTag("Coin").Length){
            _metal = GameObject.FindGameObjectsWithTag("Coin");
        }
        if (_metal.Length > 0){
            foreach (GameObject m in _metal){
                LineRenderer line;
                if(m.GetComponent<LineRenderer>()){
                    line = m.GetComponent<LineRenderer>();
                }
                else{
                    line = m.AddComponent<LineRenderer>();
                }  
                line.SetPosition(0, _player.transform.position);
                line.SetPosition(1, m.transform.position);
                line.startWidth = 0.01f;
                line.endWidth = 0.01f;
                line.material = _cyan;
            }
            sort();
            if (_selected == null)
                select(_metal[0]);
        }
    }
    void select(GameObject go){
        _selected = go;
        var outline = go.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.cyan;
        outline.OutlineWidth = 10f;

        try {
            var line = go.GetComponent<LineRenderer>();
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
        }
        catch (MissingComponentException){ }
    }
    public void deSelect(GameObject _selected){
        Destroy(_selected.GetComponent<Outline>());
        Destroy(_selected.GetComponent<LineRenderer>());
    }
    public void deSelector(){
        _selected = null;
        foreach (GameObject m in _metal){
            deSelect(m);
        }
        _metal = new GameObject[]{};
    }
    public void coinChanger(int value){
        if(_metal.Length > 1){
            deSelect(_selected);
            int id = 0;
            for (int j = 0; j < _metal.Length; j++) {
                if (_metal[j] == _selected){
                    id = j;
                }
            }
            if (value == 1){
                id++; // +x
            }
            else
                id--; // -x
            if(id < 0) id = _metal.Length-1;
            if(id >= _metal.Length) id = 0;
            // print(id);
            select(_metal[id]);
        }
    }
    void sort(){
        GameObject temp;
        for (int j = 0; j <= _metal.Length - 2; j++) {
            for (int i = 0; i <= _metal.Length - 2; i++) {
                if (Vector3.Distance(_player.transform.position, _metal[i].transform.position) > Vector3.Distance(_player.transform.position, _metal[i +1].transform.position)){
                    temp = _metal[i + 1];
                    _metal[i + 1] = _metal[i];
                    _metal[i] = temp;
                }
            }
        }
    }
    public GameObject getSelected(){return _selected;}
}
