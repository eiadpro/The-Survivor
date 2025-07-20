using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(-31.61f, 0f, 69.1f);
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
