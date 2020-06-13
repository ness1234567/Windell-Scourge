using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : MonoBehaviour
{
    ItemData _itemData;
    int _quantity;

    public ItemData itemData
    {
        get { return _itemData; }
        set { _itemData = value; }
    }

    public int quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public virtual void use() { }
}
