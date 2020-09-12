using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIScreen
{
    HUD = 0,
    Inventory = 1,
    Settings = 2,
    Chest = 3,
}

public class ScreenUI : MonoBehaviour
{
    private static ScreenUI _instance;
    private UIScreen currentUIScreen = UIScreen.HUD;

    public GameObject HUDScreen;
    public GameObject InventoryScreen;
    public GameObject settingScreen;
    public GameObject ChestScreen;

    public static ScreenUI Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
            if (currentUIScreen == UIScreen.Inventory)
            {
                CloseInventory();
                currentUIScreen = UIScreen.HUD;
            }
            else 
            {
                OpenInventory();
                currentUIScreen = UIScreen.Inventory;
            }
        }
    }

    //Check if the settings screen is open
    private void checkPauseKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentUIScreen == UIScreen.Settings)
            {
                CloseSettings();
            }
            else if (currentUIScreen == UIScreen.HUD)
            {
                OpenSettings();
            } else
            {
                closeAllUIScreens();
                HUDScreen.SetActive(true);
            }
        }
    }

    public void CloseInventory()
    {
        closeAllUIScreens();
        HUDScreen.SetActive(true);
    }

    public void OpenInventory()
    {
        closeAllUIScreens();
        InventoryScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        closeAllUIScreens();
        HUDScreen.SetActive(true);
        //Time.timeScale = 1f;
        currentUIScreen = UIScreen.HUD;
    }

    public void OpenSettings()
    {
        closeAllUIScreens();
        settingScreen.SetActive(true);
        //Time.timeScale = 0f;
        currentUIScreen = UIScreen.Settings;
    }

    public void CloseChest()
    {
        closeAllUIScreens();
        HUDScreen.SetActive(true);
        currentUIScreen = UIScreen.HUD;
    }

    public void OpenChest(GameObject obj)
    {
        closeAllUIScreens();
        ChestScreen.GetComponent<ChestUI>().setChest(obj.GetComponentInParent<ChestStorage>());

        ChestScreen.SetActive(true);

        //Time.timeScale = 0f;
        currentUIScreen = UIScreen.Chest;
    }


    public void closeAllUIScreens()
    {
        InventoryScreen.SetActive(false);
        ChestScreen.SetActive(false);
        settingScreen.SetActive(false);
        HUDScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
