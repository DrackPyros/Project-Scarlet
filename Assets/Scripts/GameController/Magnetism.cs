using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{
    private GameObject _player, _coin;
    private Rigidbody _playerRb, _coinRb;
    private float _attract = 0, _repel = 0;
    void Start(){
        _player = GameObject.Find("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
    }
    void Update(){
        if(_attract > 0){
            pull(_attract);
        }
        else if(_repel < 0){
            push(_repel);
        }
        // print(_playerRb.velocity.y);
    }
    public void push(float force){ //TODO: Arreglar
        _repel = force;
        setSelectedCoin();
        if (_coin != null){
            float efectArea = Vector3.Distance(_player.transform.position, _coin.transform.position);
            if (efectArea <= 20){
                // print(force);
                if(_coin.GetComponent<CoinAnimation>().ongroud()){
                    // print("entra");
                    // _playerRb.AddForce(-_coin.transform.position, ForceMode.Force);
                    // _player.transform.position = Vector3.MoveTowards(_player.transform.position, _coin.transform.position, force * _playerRb.mass * Time.deltaTime);
                    _player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(_player.transform.position - _coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                    // print(((Vector3.Normalize(_player.transform.position - _coin.transform.position)* force)/efectArea));
                } else{
                    // print(2);
                    _player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(_player.transform.position - _coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                    _coin.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(_coin.transform.position - _player.transform.position)* force)/efectArea), ForceMode.Impulse);
                }
                // if choca contra el suelo
                // push recto
            }
        }
    }
    public void pull(float force){ //TODO: Nivelar
        _attract = force;
        setSelectedCoin();
        if (_coin != null){
            float efectArea = Vector3.Distance(_player.transform.position, _coin.transform.position);
            if (efectArea <= 20){
                // print(force);
                _coin.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(_player.transform.position - _coin.transform.position)* force)/efectArea), ForceMode.Impulse);
                _player.GetComponent<Rigidbody>().AddForce(((Vector3.Normalize(_coin.transform.position - _player.transform.position)* force)/efectArea), ForceMode.Impulse);
                // print(((Vector3.Normalize(_coin.transform.position - _player.transform.position)* force)/efectArea));
            }
        }
    }
    void setSelectedCoin(){
        try{
            _coin = gameObject.GetComponent<LineViewer>().getSelected();
            _coinRb = _coin.GetComponent<Rigidbody>();
        }
        catch {}
    }
    public void nullForce(bool opcion){
        _attract = opcion ? 0 : _attract;
        _repel = opcion ? _repel : 0;
    }
}
