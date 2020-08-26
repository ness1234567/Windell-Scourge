using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggedItemUI : MonoBehaviour
{
    [SerializeField]
    private Image im;
    private ItemStack _draggedItemStack;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
        im.color = disabledColor;
        _draggedItemStack = null;
    }

    public ItemStack itemStack
    {
        get { return _draggedItemStack; }
        set
        {
            _draggedItemStack = value;
            if (_draggedItemStack != null)
            {
                im.sprite = _draggedItemStack.Item.img;
                im.color = normalColor;
            }
            else
            {
                im.sprite = null;
                im.color = disabledColor;
            }
        }
    }

    public Item Item
    {
        get { return _draggedItemStack.Item; }
        set { }
    }
}
