using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryUI : MonoBehaviour
{
    public event Action<ItemObject> OnRightClickItemEvent;

    [SerializeField]
    InventoryObject inventory;
    [SerializeField]
    Transform itemSlotsObject;
    [SerializeField]
    SlotUI[] itemSlots;

    private void Awake()
    {
        for(int i = 0; i < inventory.totalSlots; i++)
        {
            itemSlots[i].OnRightClickItemEvent += OnRightClickItemEvent;
        }
    }

    private void OnEnable()
    {
        Debug.Log("Refreshing!");
        if (itemSlotsObject != null)
        {
            itemSlots = itemSlotsObject.GetComponentsInChildren<SlotUI>();
        }
        RefreshUI();
    }

    public void RefreshUI()
    {
        for(int i = 0; i < inventory.totalSlots && i < itemSlots.Length; i++)
        {
            if (inventory.getItem(i) != null)
            { 
                itemSlots[i].item = inventory.getItem(i);
            } else
            {
                itemSlots[i].item = null;
            }
        }

    }
}
