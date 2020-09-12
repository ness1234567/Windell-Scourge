using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ChestUI : MonoBehaviour, IUserInterface
{
    //list of observers to notify on right-click/enter/exit any item in inventory
    public event Action<SlotUI, IContainer, IUserInterface> OnLeftClickItemEvent;
    public event Action<SlotUI> OnPointerEnterItemEvent;
    public event Action<SlotUI> OnPointerExitItemEvent;
    public event Action OnPointerClickOutside;

    RectTransform rt;
    InventoryObject inventory;
    ChestStorage chest;

    [SerializeField]
    ItemHighlighUI highlight;

    [SerializeField]
    Transform InvSlotsObject;
    [SerializeField]
    Transform ChestSlotsObject;

    [SerializeField]
    SlotUI[] itemSlotsInv;
    [SerializeField]
    SlotUI[] itemSlotsChest;

    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryController.Instance.Inventory;
        itemSlotsInv = InvSlotsObject.GetComponentsInChildren<SlotUI>();
        itemSlotsChest = ChestSlotsObject.GetComponentsInChildren<SlotUI>();

        rt = gameObject.GetComponent<RectTransform>();

        //Each slot is an observable. Subscribe to each observerable to check for a right click event on a slot
        for (int i = 0; i < inventory.totalSlots; i++)
        {
            itemSlotsInv[i].SlotID = i;
            itemSlotsInv[i].OnLeftClickEvent += OnLeftClickItemEventWrapper_Inventory;
            itemSlotsInv[i].OnPointerEnterEvent += CursorEnterItem;
            itemSlotsInv[i].OnPointerExitEvent += CursorExitItem;
        }

        for (int i = 0; i < 30; i++)
        {
            itemSlotsChest[i].SlotID = i;
            itemSlotsChest[i].OnLeftClickEvent += OnLeftClickItemEventWrapper_Chest;
            itemSlotsChest[i].OnPointerEnterEvent += CursorEnterItem;
            itemSlotsChest[i].OnPointerExitEvent += CursorExitItem;
        }
        Refresh();

    }

    private void Update()
    {
        detectClickOutside();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (inventory == null)
            return;

        for (int i = 0; i < inventory.totalSlots; i++)
        {
            if (inventory.getItem(i) != null)
            {
                itemSlotsInv[i].item = inventory.getItem(i);
                itemSlotsInv[i].Image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                itemSlotsInv[i].item = null;
            }
        }

        for (int i = 0; i < 30; i++)
        {
            if (chest.getItem(i) != null)
            {
                itemSlotsChest[i].item = chest.getItem(i);
                itemSlotsChest[i].Image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                itemSlotsChest[i].item = null;
            }
        }

        highlight.deactivateHighlight();

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


    private void CursorEnterItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1.25f, 1.25f, 1);

        Vector3 pos = i.transform.localPosition;

        float biasx = 0;
        float biasy = 0;
        if ((i.SlotID >= 0) && (i.SlotID <= 29))
        {
            biasx = -0.5f;
            biasy = 0;
        }
        else if ((i.SlotID >= 30) && (i.SlotID <= 39))
        {
            biasx = 0;
            biasy = 0;
        }
        else if ((i.SlotID >= 40) && (i.SlotID <= 69))
        {
            biasx = -0.5f;
            biasy = 0;
        }

        highlight.activateHighlight(pos.x + biasx, pos.y + biasy, 1, 1);

    }

    private void CursorExitItem(SlotUI i)
    {
        i.Image.rectTransform.localScale = new Vector3(1, 1, 1);
        //highlight = InvUI.GetComponentInChildren<ItemHighlighUI>();
        highlight.deactivateHighlight();
    }


    public void setChest(ChestStorage clickedChest)
    {
        chest = clickedChest;
    }

    private void OnLeftClickItemEventWrapper_Chest(SlotUI i)
    {
        OnLeftClickItemEvent(i, chest, this);
    }

    private void OnLeftClickItemEventWrapper_Inventory(SlotUI i)
    {
        OnLeftClickItemEvent(i, inventory, this);
    }
}
