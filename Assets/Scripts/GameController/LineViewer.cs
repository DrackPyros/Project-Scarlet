using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineViewer : MonoBehaviour
{
    private GameObject[] metal = new GameObject[]{};
    public GameObject player;
    private GameObject selected = null;
    public Material cyan;
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            viewer();
            if (metal.Length > 0){
                sort();
                select(metal[0]);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            viewer();
            if (metal.Length > 0){
                sort();
                if (Input.GetAxisRaw("Horizontal") != 0){
                    coinChanger();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            foreach (GameObject m in metal){
                deSelect(m);
            }
        }
    }
    void viewer(){
        if (metal.Length < GameObject.FindGameObjectsWithTag("Coin").Length){
            metal = GameObject.FindGameObjectsWithTag("Coin");
            // if (metal.Length == 1)
            //     select(metal[0]);
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
    void coinChanger(){
        deSelect(selected);
        int id = 0;
        for (int j = 0; j < metal.Length; j++) {
            if (metal[j] == selected){
                id = j;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 1){
            id++; // +x
        }
        else
            id--; // -x
        if(id < 0) id = metal.Length;
        if(id > metal.Length) id = 0;
        select(metal[id]);
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
}
