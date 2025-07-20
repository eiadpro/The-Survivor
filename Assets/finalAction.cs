using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalAction : MonoBehaviour
{
    bool action = false;
    [SerializeField] Transform[] floor;
    [SerializeField] AudioSource a;
    [SerializeField] AudioSource a1;
    [SerializeField] AudioSource af;
    int cur = 0;
    bool done = false;
    int r = 0;
    int how = 0;
    bool once = true;
    bool once1 = true;
    bool once2 = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {

            if (once2)
            {
                af.Play();
                once2 = false;
            }
            if (floor[cur].position.y <= -53)
            {
                floor[cur].GetComponent<MeshRenderer>().enabled = false;
                floor[cur].GetComponent<BoxCollider>().enabled = false;
                cur++;
                if (cur == floor.Length)
                    cur = 0;
                 r = Random.Range(4, floor.Length-1);
                if (floor[r].position.y > -20&&floor[r-1].position.y > -20&& floor[r + 1].position.y > -20) {
                    done = true;
                    how++;
                }
                else
                {
                    done = false;
                }
                if(floor[10].position.y > -50 && floor[9].position.y > -50)
                {
                    if (once1) {
                        a1.Play();
                        once1 = false;
                    }
                    
                }

            }
            floor[cur].Translate(0f,-36f*Time.deltaTime,0f);
            if (done && how <= 2)
            {
                floor[r].Translate(0f, -36f * Time.deltaTime, 0f);
                if (floor[r].position.y <= -53)
                {
                    floor[r].GetComponent<MeshRenderer>().enabled = false;
                    floor[r].GetComponent<BoxCollider>().enabled = false;
                }
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (once)
            {
                a.Play();
                once = false;
            }
            action = true;
        }

    }
}
