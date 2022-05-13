using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _offset;
    void Start(){
        _player = GameObject.Find("Player");
        _offset = transform.position;
    }
    void LateUpdate(){
        transform.position = _player.transform.position + _offset;
    }
}
