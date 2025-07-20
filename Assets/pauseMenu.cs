using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausemenu;
    public GameObject settingsCanvas;
    public Transform character;



    public void QuitToMainMenu()
    {

        SceneManager.LoadScene(0);
    }
    public void Restart()
    {

        SceneManager.LoadScene(1);
    }
    public void OpenSettings()
    {

        pausemenu.SetActive(false);
        settingsCanvas.SetActive(true);
    }
}
