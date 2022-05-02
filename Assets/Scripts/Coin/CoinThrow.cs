using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    private float x, y;
    private int force = 150;

    void Start()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        x = x * force;
        y = y * force;
        gameObject.GetComponent<Rigidbody>().AddForce(x * Time.deltaTime, y * Time.deltaTime, 0, ForceMode.Impulse);
        // print(x +" - "+ y);
        // print((x * Time.deltaTime) + " - " + ((x+y) * Time.deltaTime));
        // Destroy(gameObject, 5);
    }
}