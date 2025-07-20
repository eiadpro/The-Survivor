using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waybut : MonoBehaviour
{
    [SerializeField] Transform[] ways;
    [SerializeField] Transform but1;
    [SerializeField] Transform but2;
    int current = 0;
    bool once = false;
    bool once1 = false;
    bool once2 = false;
    [SerializeField] AudioSource af5;
    [SerializeField] AudioSource af6;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (af5 != null)
        {
            if (but1.GetComponent<butcol>().but1)
            {
                if (!once)
                {
                    af5.Play();
                    once = true;
                }
            }
            if (but2.GetComponent<butcol>().but1)
            {
                if (!once1)
                {
                    af5.Play();
                    once1 = true;
                }
            }
        }
        if (but1.GetComponent<butcol>().but1 && but2.GetComponent<butcol>().but1)
        {
            if (af6 != null)
            {
                if (!once2)
                {
                    af6.Play();
                    once2 = true;
                }
            }
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
}
