using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBehaviour : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO
    public void onClick()
    {
        Debug.Log("My click detection");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Unitys click event");
    }
}
