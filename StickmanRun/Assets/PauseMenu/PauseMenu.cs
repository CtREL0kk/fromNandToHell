using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isOnPause;
    public GameObject pauseMenu;
    public GameObject pauseWindow;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOnPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseWindow.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isOnPause = false;
    }

    public void Pause()
    {
        pauseWindow.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isOnPause = true;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Menu");
    }
}
