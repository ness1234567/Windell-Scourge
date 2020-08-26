using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    Collider2D c;

    // Start is called before the first frame update
    void Start()
    {
        c = this.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Player")
        {
            return;
        }

        Debug.Log("OnCollisionEnter2D");
        InventoryController invControl = InventoryController.Instance;

        DroppedItem i = this.GetComponent<DroppedItem>();
        Debug.Log(invControl);

        int numleft = invControl.AutoStackItem(i.Item, i.Qauntity);
        if (numleft == 0)
        {
            //Destroy the collided object
            Destroy(gameObject);
        } else
        {
            i.Qauntity = numleft;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
