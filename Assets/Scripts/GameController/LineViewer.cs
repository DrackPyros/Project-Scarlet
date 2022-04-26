using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineViewer : MonoBehaviour
{
    private GameObject[] metal;
    public GameObject player;
    public Material glow;
    void Update(){
        if (Input.GetKey(KeyCode.LeftShift)){
            viewer();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            // get();
            getClosest();
        }
    }
    void viewer(){ // Inicializar a != null
        if (metal != GameObject.FindGameObjectsWithTag("Coin")){ //No va
            metal = GameObject.FindGameObjectsWithTag("Coin");
            print(metal.Length);
        }
    }
    void get(){
        print(metal[0].transform.position);
    }
    void getClosest(){
        GameObject id = null;
        float auxDistance = 100f;
        foreach (GameObject aux in metal){
            if (Vector3.Distance(player.transform.position, aux.transform.position)< auxDistance){
                id = aux;
                auxDistance = Vector3.Distance(player.transform.position, aux.transform.position);
            }
        }
        print(id.transform.position);
        id.GetComponent<Renderer>().materials[1] = glow;
    }
}
