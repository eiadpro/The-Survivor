using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butcol : MonoBehaviour
{
    public bool but1;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            but1 = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            but1 = false;
        }
    }
}
