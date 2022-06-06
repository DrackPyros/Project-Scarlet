using UnityEngine;

public class FollowPlayer : MonoBehaviour{
    private GameObject _player;
    private Vector3 _offset;
    private bool _follow = false;
    
    void Start(){
        _player = GameObject.Find("Player");
        _offset = new Vector3(0, 4, -10);
    }
    void LateUpdate(){
        if(_player.transform.position.x >= (Vector3.right.x*10)){
            _follow = true;
        }
        else
            _follow = false;

        if (_follow){
            transform.position = _player.transform.position + _offset;
            if(transform.position.y < 4){ transform.position = new Vector3(transform.position.x, 4, transform.position.z);}
        }
    }
}
