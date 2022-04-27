using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineViewer : MonoBehaviour
{
    private GameObject[] metal = new GameObject[]{};
    public GameObject player;
    private GameObject id = null;
    public Material cyan;
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            if (metal.Length > 0){
                viewer();
                getClosest();
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            viewer();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            foreach (GameObject m in metal){
                Destroy(m.GetComponent<Outline>());
                Destroy(m.GetComponent<LineRenderer>());
            }
        }
    }
    void viewer(){
        if (metal.Length < GameObject.FindGameObjectsWithTag("Coin").Length){
            metal = GameObject.FindGameObjectsWithTag("Coin");
            if (metal.Length == 1)
                getClosest();

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
    void get(){
        print(metal[0].transform.position);
    }
    void getClosest(){
        float auxDistance = 100f;
        foreach (GameObject aux in metal){
            if (Vector3.Distance(player.transform.position, aux.transform.position)< auxDistance){
                id = aux;
                auxDistance = Vector3.Distance(player.transform.position, aux.transform.position);
            }
        }
        var outline = id.AddComponent<Outline>();
        // print(id.transform.position);
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.cyan;
        outline.OutlineWidth = 10f;

        try {
            var line = id.GetComponent<LineRenderer>();
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
        }
        catch (MissingComponentException){ }
            
    }
}
