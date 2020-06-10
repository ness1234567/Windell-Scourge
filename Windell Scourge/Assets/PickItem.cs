using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    Collider2D c;

    // Start is called before the first frame update
    void Start()
    {
        c = this.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        InventoryController invControl = InventoryController.Instance;

        ItemData item = col.gameObject.GetComponent<DroppedItem>().Item;

        invControl.PickUpItem(item);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
