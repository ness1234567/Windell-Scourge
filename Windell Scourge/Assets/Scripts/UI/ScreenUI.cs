using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUI : MonoBehaviour
{
    public static bool SettingsScreenOpen = false;
    private bool InventoryScrenOpen = false;

    public GameObject HUDScreen;
    public GameObject InventoryScreen;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInventoryKeyPressed();
        checkPauseKeyPressed();
    }

    //check if the the inventory key is presed;
    private void checkInventoryKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryScrenOpen)
            {
                CloseInventory();
                InventoryScrenOpen = false;
            }
            else
            {
                OpenInventory();
                InventoryScrenOpen = true;
            }
        }
    }

    //Check if the settings screen is open
    private void checkPauseKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsScreenOpen)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void CloseInventory()
    {
        InventoryScreen.SetActive(false);
        HUDScreen.SetActive(true);
    }

    public void OpenInventory()
    {
        InventoryScreen.SetActive(true);
        HUDScreen.SetActive(false);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        HUDScreen.SetActive(true);
        Time.timeScale = 1f;
        SettingsScreenOpen = false;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        HUDScreen.SetActive(false);
        InventoryScreen.SetActive(false);
        //Time.timeScale = 0f;
        InventoryScrenOpen = false;
        SettingsScreenOpen = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
