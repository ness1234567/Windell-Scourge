using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLoadingOrder : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    private void OnEnable()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

}
