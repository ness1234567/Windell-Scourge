using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//Displays the inventory to the player and handles input (via SlotUI)
public class InventoryUI : MonoBehaviour, IUserInterface
{
    //list of observers to notify on right-click any item in inventory
    public event Action<SlotUI, IContainer, IUserInterface> OnLeftClickItemEvent;
    public event Action OnPointerClickOutside;

    InventoryObject inventory;
    [SerializeField]
    Transform InvSlotsObject;
    [SerializeField]
    SlotUI[] itemSlots;
    [SerializeField]
    ItemHighlighUI highlight;
    RectTransform rt;

    private void Start()
    {
        inventory = InventoryController.Instance.Inventory;
        itemSlots = InvSlotsObject.GetComponentsInChildren<SlotUI>();
        rt = gameObject.GetComponent<RectTransform>();

        //Each slot is an observable. Subscribe to each observerable to check for a right click event on a slot
        for (int i = 0; i < inventory.totalSlots; i++)
        {
            itemSlots[i].SlotID = i;
            itemSlots[i].OnLeftClickEvent += OnLeftClickItemEventWrapper;
            itemSlots[i].OnPointerEnterEvent += main_CursorEnterItem;
            itemSlots[i].OnPointerExitEvent += main_CursorExitItem;
        }
        OpenRefresh();

    }

    private void Update()
    {
        detectClickOutside();
    }

    private void OnEnable()
    {
        OpenRefresh();
    }

    private void OnDisable()
    {
        OpenRefresh();
    }

    public void OpenRefresh()
    {
        if (inventory == null)
            return;

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            if (inventory.getItem(i) != null)
            {
                itemSlots[i].item = inventory.getItem(i);
                itemSlots[i].Image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                itemSlots[i].item = null;
            }
        }
        highlight.deactivateHighlight();
    }

    public void Refresh()
    {
        if (inventory == null)
            return;

        for (int i = 0; i < inventory.totalSlots; i++)
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

    private void detectClickOutside()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                rt,
                Input.mousePosition,
                Camera.main))
        {
            OnPointerClickOutside();
        }
    }

    private void main_CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);

        Vector3 pos = i.transform.localPosition;

        float biasx = 0;
        float biasy = 0;
        if ((i.SlotID >= 0) && (i.SlotID <= 29))
        {
            biasx = -109.5f;
            biasy = 80;
        }
        else if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            biasx = -109.5f;
            biasy = 62;
        }

        highlight.activateHighlight(pos.x + biasx, pos.y + biasy, 1, 1);

    }

    private void main_CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        //highlight = InvUI.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight();
    }

    private void OnLeftClickItemEventWrapper(SlotUI i)
    {
        OnLeftClickItemEvent(i, inventory, this);
    }

}
