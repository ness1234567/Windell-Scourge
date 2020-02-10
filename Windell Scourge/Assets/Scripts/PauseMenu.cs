using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;
    public GameObject HUDUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        settingMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        HUDUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        HUDUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
