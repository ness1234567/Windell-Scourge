using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreenUI : MonoBehaviour
{
    private bool InventoryOpen = false;
    public GameObject InventoryScreen;
    public GameObject HUDUI;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryOpen)
            {
                CloseInventory();
                InventoryOpen = false;
            }
            else
            {
                OpenInventory();
                InventoryOpen = true;
            }
        }
    }

    public void CloseInventory()
    {
        InventoryScreen.SetActive(false);
        HUDUI.SetActive(true);
    }

    public void OpenInventory()
    {
        InventoryScreen.SetActive(true);
        HUDUI.SetActive(false);
    }
}
