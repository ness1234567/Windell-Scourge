using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //List of subscribers for a left click event
    public event Action<SlotUI> OnLeftClickEvent;
    public event Action<SlotUI> OnPointerEnterEvent;
    public event Action<SlotUI> OnPointerExitEvent;
    //public event EventHandler OnPointerExitEvent2;

    private int _SlotID;
    [SerializeField]
    private ItemObject _item;
    [SerializeField]
    private Image _img;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(0, 0, 0, 0);

    private void OnValidate()
    {
        if (_img == null)
        {
            _img = GetComponent<Image>();
        }
    }

    public int SlotID
    {
        get { return _SlotID; }
        set { _SlotID = value;}
    }

    public ItemObject item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                _img.color = disabledColor;
            } else
            {
                _img.sprite = _item.img;
                _img.color = normalColor;
            }
        } 
    }

    public Image Image
    {
        get { return _img; }
    }

    private void Awake()
    {
        
    }

    //observable notify function handler. Notfies observers/subscribers that slot was left clicked.
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData != null) && (eventData.button == PointerEventData.InputButton.Left) && (OnLeftClickEvent != null))
        {
            OnLeftClickEvent(this);
        }
    }

    //observable notify function handler. Notfies observers/subscribers that the mouse hovered over slot.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((eventData != null) && (OnPointerEnterEvent != null)) 
        {
            OnPointerEnterEvent(this);
        }
    }

    //observable notify function handler. Notfies observers/subscribers that the mouse exited slot.
    public void OnPointerExit(PointerEventData eventData)
    {
        if ((eventData != null) && (OnPointerExitEvent != null))
        {
            OnPointerExitEvent(this);
            //OnPointerExitEvent2(this, EventArgs.Empty);
        }
    }

    public void debug()
    {
        Debug.Log(OnLeftClickEvent);
    }
}
