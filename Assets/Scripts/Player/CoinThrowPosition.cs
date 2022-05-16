using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrowPosition : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    public void coinPosition(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0){
            Instantiate(_coin, transform.position + Vector3.right, Quaternion.identity);
        }
        else
            Instantiate(_coin, transform.position + new Vector3(horizontal, vertical, 0), Quaternion.identity);

    }
}