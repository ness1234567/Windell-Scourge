﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryUI : MonoBehaviour
{
    //list of observers to notify on right click
    public event Action<SlotUI> OnLeftClickItemEvent;
    public event Action<SlotUI> OnPointerEnterItemEvent;
    public event Action<SlotUI> OnPointerExitItemEvent;

    [SerializeField]
    InventoryObject inventory;
    [SerializeField]
    Transform InvSlotsObject;
    [SerializeField]
    SlotUI[] itemSlots;

    private void Awake()
    {
        itemSlots = InvSlotsObject.GetComponentsInChildren<SlotUI>();

        //Each slot is an observable. Subscribe to each observerable to check for a right click event on a slot
        for(int i = 0; i < inventory.totalSlots; i++)
        {
            itemSlots[i].SlotID = i;
            itemSlots[i].OnLeftClickEvent += OnLeftClickItemEvent;
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterItemEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitItemEvent;
        }
    }

    private void OnEnable()
    {
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        Debug.Log("Refreshing!");
        for (int i = 0; i < inventory.totalSlots; i++)
        {
            if (inventory.getItem(i) != null)
            { 
                itemSlots[i].item = inventory.getItem(i);
                itemSlots[i].Image.rectTransform.localScale = new Vector3(1,1,1);
            } else
            {
                itemSlots[i].item = null;
            }
        }

    }
}