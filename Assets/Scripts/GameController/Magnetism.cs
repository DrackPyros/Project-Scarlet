using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{
    private GameObject player, coin;
    private Rigidbody playerRb, coinRb;

    void Start(){
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }
    public void push(float atraer){
        // print(atraer);
        setSelectedCoin();
        if (coin != null){
            float efectArea = Vector3.Distance(player.transform.position, coin.transform.position);
            if (efectArea <= 20){
                if(coin.GetComponent<CoinAnimation>().ongroud()){
                    playerRb.AddForce(-coin.transform.position, ForceMode.Force);
                    // player.transform.position = Vector3.MoveTowards(player.transform.position, coin.transform.position, atraer * playerRb.mass * Time.deltaTime);
                } else{
                    player.transform.position = Vector3.MoveTowards(player.transform.position, coin.transform.position, atraer * coinRb.mass * Time.deltaTime);
                    coin.transform.position = Vector3.MoveTowards(coin.transform.position, player.transform.position, atraer * playerRb.mass * Time.deltaTime);
                }
                // if choca contra el suelo
                // push recto
             }
        }
    }
    public void pull(float atraer){
        print(atraer);
        setSelectedCoin();
        if (coin != null){
            float efectArea = Vector3.Distance(player.transform.position, coin.transform.position);
            if (efectArea <= 20){
                player.transform.position = Vector3.MoveTowards(player.transform.position, coin.transform.position, atraer * coinRb.mass * Time.deltaTime);
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, player.transform.position, atraer * playerRb.mass * Time.deltaTime);
                // clipear contra pj
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
}
