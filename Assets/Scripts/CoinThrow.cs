using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    public GameObject coin;
    public int x;
    public int y;

     void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(coin, new Vector3(1, 1, 0), Quaternion.identity);
            // coin.GetComponent<Rigidbody>().AddForce(0, x+y, y, ForceMode.Impulse);
        }
    }
}
