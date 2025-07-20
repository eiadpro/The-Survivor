using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class way : MonoBehaviour
{
    [SerializeField] Transform[] ways;
    int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, ways[current].position) < 0.1)
        {
            current++;
            if (current >= ways.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, ways[current].position, 8 * Time.deltaTime);
    }
}
