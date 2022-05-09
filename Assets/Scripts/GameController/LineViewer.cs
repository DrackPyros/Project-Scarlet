using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineViewer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Material cyan;
    private GameObject[] metal = new GameObject[]{};
    private GameObject selected = null;
    private bool watch = false;
    
    void Update(){
        if (watch)
            viewer();
    }
    public void setWatch(bool value){
        watch = value;
    }
    public void viewer(){
        if (metal.Length < GameObject.FindGameObjectsWithTag("Coin").Length){
            metal = GameObject.FindGameObjectsWithTag("Coin");
        }
        if (metal.Length > 0){
            foreach (GameObject m in metal){
                LineRenderer line;
                if(m.GetComponent<LineRenderer>()){
                    line = m.GetComponent<LineRenderer>();
                }
                else{
                    line = m.AddComponent<LineRenderer>();
                }  
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, m.transform.position);
                line.startWidth = 0.01f;
                line.endWidth = 0.01f;
                line.material = cyan;
            }
            sort();
            if (selected == null)
                select(metal[0]);
        }
    }
    void select(GameObject go){
        selected = go;
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
    void deSelect(GameObject selected){
        Destroy(selected.GetComponent<Outline>());
        Destroy(selected.GetComponent<LineRenderer>());
    }
    public void deSelector(){
        selected = null;
        foreach (GameObject m in metal){
            deSelect(m);
        }
    }
    public void coinChanger(int value){
        if(metal.Length > 1){
            deSelect(selected);
            int id = 0;
            for (int j = 0; j < metal.Length; j++) {
                if (metal[j] == selected){
                    id = j;
                }
            }
            if (value == 1){
                id++; // +x
            }
            else
                id--; // -x
            if(id < 0) id = metal.Length-1;
            if(id >= metal.Length) id = 0;
            // print(id);
            select(metal[id]);
        }
    }
    void sort(){
        GameObject temp;
        for (int j = 0; j <= metal.Length - 2; j++) {
            for (int i = 0; i <= metal.Length - 2; i++) {
               if (Vector3.Distance(player.transform.position, metal[i].transform.position) > Vector3.Distance(player.transform.position, metal[i +1].transform.position)){
                  temp = metal[i + 1];
                  metal[i + 1] = metal[i];
                  metal[i] = temp;
               }
            }
        }
    }
    public GameObject getSelected(){return selected;}
}
