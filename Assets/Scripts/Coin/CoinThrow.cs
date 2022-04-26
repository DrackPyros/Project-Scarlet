using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    private float x, y;

    void Start()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        gameObject.GetComponent<Rigidbody>().AddForce(x * Time.deltaTime, (x+y) * Time.deltaTime, 0, ForceMode.Impulse);
        // Destroy(gameObject, 5);
    }
}