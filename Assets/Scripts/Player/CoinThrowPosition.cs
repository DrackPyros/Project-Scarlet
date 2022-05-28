using UnityEngine;

public class CoinThrowPosition : MonoBehaviour{
    [SerializeField] private GameObject _coin;

    public void CoinPosition(float horizontal, float vertical){
        if (horizontal == 0 && vertical == 0){
            Instantiate(_coin, (transform.position + Vector3.up) + Vector3.right, Quaternion.identity);
        }
        else
            Instantiate(_coin, (transform.position + Vector3.up) + new Vector3(horizontal, vertical, 0), Quaternion.identity);
    }
}
