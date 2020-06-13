using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemData _Item;
    int _Qauntity;
    float x;
    float y;

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

    public ItemData Item {
        get { return _Item; }
        set { _Item = value; }
    }

    public int Qauntity
    {
        get { return _Qauntity; }
        set { _Qauntity = value; }
    }

}
