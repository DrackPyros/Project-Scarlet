using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{
    private GameObject _player, _coin;
    private Rigidbody _playerRb, _coinRb;
    private float _attract = 0, _repel = 0;
    // private const int _multiply = 0;
    private const int _frame = 60;
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
                if(_coin.GetComponent<CoinAnimation>().ongroud()){
                    _player.GetComponent<Rigidbody>().AddForce(((_coin.transform.position - _player.transform.position)* (force * (_playerRb.mass / _coinRb.mass)))* _frame * Time.deltaTime, ForceMode.Impulse);
                    // print(((_coin.transform.position - _player.transform.position)* (force * (_playerRb.mass / _coinRb.mass)))* _frame * Time.deltaTime);
                } else{
                    _player.GetComponent<Rigidbody>().AddForce(((_player.transform.position - _coin.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                    _coin.GetComponent<Rigidbody>().AddForce(((_coin.transform.position - _player.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                }
            }
        }
    }
    public void pull(float force){ //TODO: Inversa para objetos grandes
        _attract = force;
        setSelectedCoin();
        if (_coin != null){
            float efectArea = Vector3.Distance(_player.transform.position, _coin.transform.position);
            if (efectArea <= 20){
                if (_coin.transform.position != _player.transform.position){
                    _coin.GetComponent<Rigidbody>().AddForce(((_player.transform.position - _coin.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                    _player.GetComponent<Rigidbody>().AddForce(((_coin.transform.position - _player.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                    // print(((Vector3.Normalize(_coin.transform.position - _player.transform.position)* force)/efectArea));
                }
                else
                    _coinRb.velocity = Vector3.zero;
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
