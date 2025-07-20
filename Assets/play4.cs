using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play4 : MonoBehaviour
{
[SerializeField] Transform p;
    bool once=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (p.position.y < 0&& once)
        {
            transform.GetComponent<AudioSource>().Play();
            once = false;
        }
    }
}
