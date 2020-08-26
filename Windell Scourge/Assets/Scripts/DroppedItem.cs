using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public Item _Item;
    int _Qauntity;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = _Item.img;
        this.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 0);
        _Qauntity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item Item
    {
        get { return _Item; }
        set { _Item = value; }
    }

    public int Qauntity
    {
        get { return _Qauntity; }
        set { _Qauntity = value; }
    }

}
