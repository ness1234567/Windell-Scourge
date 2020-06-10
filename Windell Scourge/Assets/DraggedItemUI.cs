using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggedItemUI : MonoBehaviour
{
    [SerializeField]
    private Image im;
    private ItemData _draggedItem;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
        im.color = disabledColor;
        _draggedItem = null;
    }

    public ItemData item
    {
        get { return _draggedItem; }
        set
        {
            _draggedItem = value;
            if (_draggedItem != null)
            {
                im.sprite = _draggedItem.img;
                im.color = normalColor;
            } else
            {
                im.sprite = null;
                im.color = disabledColor;
            }
        }
    }


}
