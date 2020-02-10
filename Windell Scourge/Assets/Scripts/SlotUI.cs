using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public event Action<ItemObject> OnRightClickItemEvent;

    [SerializeField]
    private ItemObject _item;
    [SerializeField]
    private Image _img;

    private void OnValidate()
    {
        if (_img == null)
        {
            _img = GetComponent<Image>();
        }
    }

    public ItemObject item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                _img.enabled = false;
            } else
            {
                _img.sprite = _item.img;
                _img.enabled = true;
            }
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicking on Slot!");
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickItemEvent != null)
            {
                OnRightClickItemEvent(item);
            }
        }
    }
}
