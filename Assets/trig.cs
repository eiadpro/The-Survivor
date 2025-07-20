using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trig : MonoBehaviour
{
   [SerializeField] Transform s;
    [SerializeField] Transform enableshoot;
    [SerializeField] Transform robo;
    [SerializeField] AudioSource a1;
    [SerializeField] Transform waypoint;

    bool coll=false;
    bool sec = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (sec&&coll)
        {
            robo.position = Vector3.MoveTowards(robo.position, waypoint.position, 7 * Time.deltaTime);
            
        }
        if (robo.position.x >= 9f&& !sec)
        {
            transform.GetComponent<Target>().enabled = true;
        }
        if (coll&&!sec&& robo.position.x >= 9f)
        {
            transform.GetComponent<Target>().enabled = false;
            sec = true;
            a1.Play();
            
            Invoke(nameof(turn), 5.3f);
        }
    }
    public void turn()
    {
        enableshoot.GetComponent<BoxCollider>().enabled = false;
        if (robo.position.x >= 9f)
            s.GetComponent<Target>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            coll = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            coll = false;
        }
    }

}
