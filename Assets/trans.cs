using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class trans : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI s;
    int c= 0;
    int many = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        c++;
        if (c >= 1500)
        {
            SceneManager.LoadScene(0);
        }
        if (c % 100 == 0)
        {
            many++;
            s.text += " .";
            if (many == 4)
            {
                many = 0;
                s.text = "To be continue";
            }
        }
    }
}
