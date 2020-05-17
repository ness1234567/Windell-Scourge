using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class HUDToolbarUI : MonoBehaviour
{
    public event Action<int> selectedItemChange;
    public event Action<SlotUI> OnPointerEnterItemEvent;
    public event Action<SlotUI> OnPointerExitItemEvent;

    int currentSlotID = 0;

    [SerializeField]
    InventoryObject inventory;
    [SerializeField]
    Transform InvSlotsObject;
    [SerializeField]
    SlotUI[] itemSlots;
    [SerializeField]
    SelectionOutlineUI selectionOutline;

    private void Awake()
    {
        itemSlots = InvSlotsObject.GetComponentsInChildren<SlotUI>();

        //Each slot is an observable. Subscribe to each observerable to check for a right click event on a slot
        for (int i = 0; i < 10; i++)
        {
            itemSlots[i].SlotID = i+30;
            itemSlots[i].OnLeftClickEvent += itemClick;
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterItemEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitItemEvent;
        }
        RefreshToolbarUI();
    }

    private void OnEnable()
    {
        RefreshToolbarUI();
    }

    void Update()
    {
        int prevSlotID = currentSlotID;

        //Poll for input
        PollKeyboardInput();
        PollMouseWheel();

        //update the current selected item in inventory
        if (prevSlotID != currentSlotID)
        {
            selectedItemChange(currentSlotID);
        }

        //Update UI current tool outline position
        selectionOutline.updatePos(currentSlotID);
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

    void PollKeyboardInput()
    {
        //poll for Keyboard inpute 1-10
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSlotID = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSlotID = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSlotID = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            currentSlotID = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            currentSlotID = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            currentSlotID = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            currentSlotID = 6;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            currentSlotID = 7;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            currentSlotID = 8;
        if (Input.GetKeyDown(KeyCode.Alpha0))
            currentSlotID = 9;
    }

    void PollMouseWheel()
    {
        //poll for mouse scroll wheel input
        float mWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mWheel > 0)
        {
            currentSlotID = currentSlotID + 1;
            if (currentSlotID > 9)
            {
                currentSlotID = currentSlotID - 10;
            }
        }
        else if (mWheel < 0)
        {
            currentSlotID = currentSlotID - 1;
            if (currentSlotID < 0)
            {
                currentSlotID = currentSlotID + 10;
            }
        }
    }

    void itemClick(SlotUI i)
    {
        currentSlotID = i.SlotID - 30;
        selectedItemChange(currentSlotID);
    }
}
