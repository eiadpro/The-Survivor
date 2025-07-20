using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    [SerializeField] Transform cube;
    bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cube.GetComponent<MeshRenderer>().enabled == true&&once)
        {
            transform.GetComponent<AudioSource>().Play();
            once = false;
        }
    }
}
