using UnityEngine;

public class Magnetism : MonoBehaviour{
    private GameObject _player, _coin;
    private Rigidbody _playerRb, _coinRb;
    private float _attract = 0, _repel = 0;
    private const int _frame = 60;

    void Start(){
        _player = GameObject.Find("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
    }
    void Update(){
        if(_attract > 0){
            Pull(_attract);
        }
        else if(_repel < 0){
            Push(_repel);
        }
        // print(_playerRb.velocity.y);
    }
    public void Push(float force){ //TODO: Arreglar
        _repel = force;
        SetSelectedCoin();
        if (_coin != null){
            float efectArea = Vector3.Distance(_player.transform.position, _coin.transform.position);
            if (efectArea <= 20){
                if(_coin.GetComponent<CoinAnimation>().Ongroud()){ // Si la moneda toca el suelo, la pelota tiene que obtener impulso como si fuera la moneda
                    _player.GetComponent<Rigidbody>().AddForce(((_coin.transform.position - _player.transform.position)* (force * (_playerRb.mass / _coinRb.mass)))* _frame * Time.deltaTime * 1000, ForceMode.Force);
                    // print(((_coin.transform.position - _player.transform.position)* (force))* _frame * Time.deltaTime); // La fuerza multiplicada por la division da un resultado menor
                } else{
                    _player.GetComponent<Rigidbody>().AddForce(((_player.transform.position - _coin.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                    _coin.GetComponent<Rigidbody>().AddForce(((_coin.transform.position - _player.transform.position)* force)* _frame * Time.deltaTime, ForceMode.Impulse);
                }
            }
        }
    }
    public void Pull(float force){ //TODO: Comprobar si la fuerza es suficiente como para simular un gancho
        _attract = force;
        SetSelectedCoin();
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
    void SetSelectedCoin(){
        try{
            _coin = gameObject.GetComponent<LineViewer>().GetSelected();
            _coinRb = _coin.GetComponent<Rigidbody>();
        }
        catch {}
    }
    public void NullForce(bool opcion){
        _attract = opcion ? 0 : _attract;
        _repel = opcion ? _repel : 0;
    }
}
