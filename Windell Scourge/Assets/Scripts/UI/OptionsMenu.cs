using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Cinemachine;


public class OptionsMenu : MonoBehaviour
{

    public Dropdown resolutionDropdown;
    public Cinemachine.CinemachineVirtualCamera vcam;

    public Slider zoomSlider;

    Resolution[] availableResolutions;

    void Awake()
    {
        //Find all available resolutions and set it to current one
        List<Resolution> temp = new List<Resolution>();

        int currResIndex = 0;
        int j = 0;

        List <string> optionList = new List<string>();

        foreach (Resolution i in Screen.resolutions)
        {
            string option = i.width + " x " + i.height;

            /*//Resolution must be 16:9
            if ((i.width / 16) != (i.height / 9))
            {
                continue;
            }*/

            //Min resolution size is 960:540
            if ((i.width < 960) || (i.height < 540))
            {
                continue;
            }

            //remove repeat resolutions
            if (optionList.IndexOf(option) != -1)
            {
                continue;
            }
            
            if (i.width == Screen.currentResolution.width &&
               i.height == Screen.currentResolution.height)
            {
                currResIndex = j;
            }

            j++;
            temp.Add(i);
            Debug.Log(option);
            optionList.Add(option);

        }

        availableResolutions = temp.ToArray();
        resolutionDropdown.AddOptions(optionList);
        resolutionDropdown.value = currResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resIndex)
    {
        Resolution res = availableResolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void setFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void setVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void setZoomSlide(float zoomLevel)
    {
        vcam.m_Lens.OrthographicSize = (1-zoomLevel)*30;
        Debug.Log((1 - zoomLevel) * 30);
    }

    public void setZoomInput(string _zoomLevel)
    {
        try
        {
            float zoomLevel = float.Parse(_zoomLevel);
            vcam.m_Lens.OrthographicSize = zoomLevel;
            zoomSlider.value = zoomLevel;
        } catch
        {
            return;
        }
        
    }
}
