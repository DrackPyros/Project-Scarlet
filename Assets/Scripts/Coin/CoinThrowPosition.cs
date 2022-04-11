using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrowPosition : MonoBehaviour
{
    public GameObject coin;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Slowmotion
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
                    Instantiate(coin, transform.position + Vector3.right, Quaternion.identity);
                }
                else
                    Instantiate(coin, transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0), Quaternion.identity);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            //Stop slowmotion
        }
    }
}
