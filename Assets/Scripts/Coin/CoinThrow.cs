using UnityEngine;

public class CoinThrow : MonoBehaviour{ 
    private GameObject _player;

    void Start(){
        Ignite();
    }
    public void Ignite(){
        float x, y;
        int force = 150;
        _player = GameObject.Find("Player");
        
        x = _player.GetComponent<InputController>().getX();
        y = _player.GetComponent<InputController>().getY();
        x = x * force;
        y = y * force;
        GetComponent<Rigidbody>().AddForce(x * Time.deltaTime, y * Time.deltaTime, 0, ForceMode.Impulse);
        // Destroy(gameObject, 5);
    }
}