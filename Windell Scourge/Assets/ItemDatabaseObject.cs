using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDatabaseObject : MonoBehaviour
{
    public ItemDatabase itemsDatabase;

    private static ItemDatabaseObject _instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static ItemDatabaseObject Instance { get { return _instance; } }

    public ItemData getItembyID(int id)
    {
        return Instance.itemsDatabase.items.FirstOrDefault(i => i.item_id == id);
    }

    public ItemData getRandomItem()
    {
        return Instance.itemsDatabase.items[Random.Range(0, Instance.itemsDatabase.items.Count())];
    }
}
