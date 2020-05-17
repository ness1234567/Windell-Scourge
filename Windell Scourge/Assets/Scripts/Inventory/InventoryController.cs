﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//Handles the inventory logic and what to do with user input
public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryUI InvUI;
    [SerializeField]
    private InventoryObject InvObject;
    [SerializeField]
    private DraggedItemUI draggedItemUI;

    //temp!! REMOVE LATER!
    public ItemObject tempHoe;

    private void Awake()
    {
        Debug.Log("CALLED CONTROLLER AWAKE");

        //Initialise inventory with 1 hoe. REMOVE LATER! 
        InvObject.addItem(0, tempHoe);

        //Subscribe handlers for clicking on item
        InvUI.OnLeftClickItemEvent += ItemClick;

        //Subscribe handlers for hovering over item
        InvUI.OnPointerEnterItemEvent += CursorEnterItem;
        InvUI.OnPointerExitItemEvent += CursorExitItem;

        //InvUI.OnPointerExitItemEvent2 += test;

        InvUI.RefreshInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        ItemHighlighUI highlight = this.GetComponentInChildren<ItemHighlighUI>();

        //move highlight image to slot position
        if ((i.SlotID >= 0) && (i.SlotID <= 29))
        {
            float inv_initX = 9;
            float inv_initY = 50;

            int row = (int)(i.SlotID / 5);
            int col = i.SlotID % 5;

            float x = inv_initX + (col * 20);
            float y = inv_initY - (row * 18);

            highlight.activateHighlight(x, y, 1, 1);
        }
        else if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            float tool_initX = -91;
            float tool_initY = -69;

            float x = tool_initX + (20 * (i.SlotID - 30));

            highlight.activateHighlight(x, tool_initY, 1, 1);
        }
    }

    private void CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        ItemHighlighUI highlight = InvUI.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight(i.SlotID);
    }

    private void ItemClick(SlotUI i)
    {
        if ((i.item != null) && (draggedItemUI.item != null))
        {
            SwapItem(i);
        }
        else if ((i.item != null) && (draggedItemUI.item == null))
        {
            SelectItem(i);
        }
        else if ((i.item == null) && (draggedItemUI.item != null))
        {
            InsertItem(i);
        }
    }

    private void SwapItem(SlotUI i)
    {
        ItemObject temp = draggedItemUI.item;
        draggedItemUI.item = i.item;
        InvObject.addItem(i.SlotID, temp);
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        InvUI.RefreshInventoryUI();
    }

    private void SelectItem(SlotUI i)
    {
        Debug.Log("Selecting item");
        draggedItemUI.item = i.item;
        InvObject.removeItem(i.SlotID);
        InvUI.RefreshInventoryUI();
    }

    private void InsertItem(SlotUI i)
    {
        Debug.Log("insert");
        InvObject.addItem(i.SlotID, draggedItemUI.item);
        InvUI.RefreshInventoryUI();
        i.Image.rectTransform.localScale = new Vector3(1.125f, 1.125f, 1);
        draggedItemUI.item = null;
    }

    /*public void test(object i, EventArgs e)
    {
        Debug.Log("TEST 3");
        Debug.Log(i);
    }*/
}
