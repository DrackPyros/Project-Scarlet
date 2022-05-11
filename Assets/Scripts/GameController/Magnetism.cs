using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{
    private GameObject player, coin;
    private Rigidbody playerRb, coinRb;
    private float attract = 0, repel = 0;
    void Start(){
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }
    void Update(){
        if(attract > 0){
            pull(attract);
        }
        else if(repel < 0){
            push(repel);
        }
        // print(playerRb.velocity.y);
    }
    public void push(float force){
        repel = force;
        setSelectedCoin();
        if (coin != null){
            float efectArea = Vector3.Distance(player.transform.position, coin.transform.position);
            if (efectArea <= 20){
                // print(force);
                if(coin.GetComponent<CoinAnimation>().ongroud()){
                    // print("entra");
                    // playerRb.AddForce(-coin.transform.position, ForceMode.Force);
                    // player.transform.position = Vector3.MoveTowards(player.transform.position, coin.transform.position, force * playerRb.mass * Time.deltaTime);
                    player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(player.transform.position - coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                    // print(((Vector3.Normalize(player.transform.position - coin.transform.position)* force)/efectArea));
                } else{
                    // print(2);
                    player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(player.transform.position - coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                    coin.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(coin.transform.position - player.transform.position)* force)/efectArea), ForceMode.Impulse);
                }
                // if choca contra el suelo
                // push recto
            }
        }
    }
    public void pull(float force){
        attract = force;
        setSelectedCoin();
        if (coin != null){
            float efectArea = Vector3.Distance(player.transform.position, coin.transform.position);
            if (efectArea <= 20){
                // print(force);
                coin.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(player.transform.position - coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(coin.transform.position - player.transform.position)* force)/efectArea), ForceMode.Impulse);
                // print(((Vector3.Normalize(coin.transform.position - player.transform.position)* force)/efectArea));
            }
        }
    }
    void setSelectedCoin(){
        try{
            coin = gameObject.GetComponent<LineViewer>().getSelected();
            coinRb = coin.GetComponent<Rigidbody>();
        }
        catch {}
    }
    public void nullForce(bool opcion){
        attract = opcion ? 0 : attract;
        repel = opcion ? repel : 0;
    }
}
