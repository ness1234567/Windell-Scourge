using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    Item _item;
    int _quantity;

    public Item Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public int quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
}
