using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatemove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] way;
    [SerializeField] Transform gate1;
    [SerializeField] float speed = 5f;
     int current = 0;
    Movement m;

    // Update is called once per frame
    private void Start()
    {
        m = GetComponent<Movement>();
    }

    void Update()
    {
    
        if (m.gate)
        {
            if (Vector3.Distance(transform.position, way[current].transform.position) < 0.1f)
            {
                current++;
                if (current >= way.Length)
                {
                    current = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, way[current].transform.position, speed * Time.deltaTime);
        }
    }
}
