using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject creditsCanvas;

    public GameObject settingsCanvas;
    public UnityEngine.UI.Slider volumeSlider;

    private bool openedFromPause = false;

    private bool isPaused = false;

    void Start()
    {


        // Initialize volume slider
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }




    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
       // mainMenu.SetActive(false);
    }

    //public void OpenSettings()
    //{
    //    Debug.Log("Settings clicked");
    //}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 0f;
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        creditsCanvas.SetActive(false);
        mainMenu.SetActive(true);
    }


    //public void OpenSettings()
    //{
    //    mainMenu.SetActive(false);
    //    settingsCanvas.SetActive(true);
    //}

    public void OpenSettings(bool fromPause)
    {
        openedFromPause = fromPause;
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsCanvas.SetActive(true);
    }


    //public void BackFromSettings()
    //{
    //    settingsCanvas.SetActive(false);
    //    mainMenu.SetActive(true);
    //}

    public void BackFromSettings()
    {
        settingsCanvas.SetActive(false);

        if (openedFromPause)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }


}
