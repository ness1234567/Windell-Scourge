using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//Displays the inventory to the player and handles input (via SlotUI)
public class InventoryUI : MonoBehaviour
{
    //list of observers to notify on right-click/enter/exit any item in inventory
    public event Action<SlotUI> OnLeftClickItemEvent;
    public event Action<SlotUI> OnPointerEnterItemEvent;
    public event Action<SlotUI> OnPointerExitItemEvent;
    //public event EventHandler OnPointerExitItemEvent2;

    [SerializeField]
    InventoryObject inventory;
    [SerializeField]
    Transform InvSlotsObject;
    [SerializeField]
    SlotUI[] itemSlots;

    private void Start()
    {
        itemSlots = InvSlotsObject.GetComponentsInChildren<SlotUI>();
        //itemSlots[0].OnPointerExitEvent2 += OnPointerExitItemEvent2;

        //Each slot is an observable. Subscribe to each observerable to check for a right click event on a slot
        for (int i = 0; i < inventory.totalSlots; i++)
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
        for (int i = 0; i < inventory.totalSlots; i++)
        {
            if (inventory.getItem(i) != null)
            { 
                itemSlots[i].item = inventory.getItem(i).itemData;
                itemSlots[i].Image.rectTransform.localScale = new Vector3(1,1,1);
            } else
            {
                itemSlots[i].item = null;
            }
        }
        ItemHighlighUI highlight = GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight();

    }
}
