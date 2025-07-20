using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diemenu : MonoBehaviour
{
    public void Retry()
    {
       
        SceneManager.LoadScene(1);
        // mainMenu.SetActive(false);
    }
    public void Exit()
    {

        SceneManager.LoadScene(0);
        // mainMenu.SetActive(false);
    }

}
