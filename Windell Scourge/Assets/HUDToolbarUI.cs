using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class HUDToolbarUI : MonoBehaviour
{
    public event Action<SlotUI> OnLeftClickItemEvent;

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
        for (int i = 0; i < 10; i++)
        {
            itemSlots[i].SlotID = i+30;
            itemSlots[i].OnLeftClickEvent += OnLeftClickItemEvent;
        }
        RefreshToolbarUI();
    }

    private void OnEnable()
    {
        RefreshToolbarUI();
    }

    public void RefreshToolbarUI()
    {
        for (int i = 0; i < 10; i++)
        {
            if (inventory.getItem(i+30) != null)
            {
                itemSlots[i].item = inventory.getItem(i+30);
                itemSlots[i].Image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                itemSlots[i].item = null;
            }
        }

    }
}
