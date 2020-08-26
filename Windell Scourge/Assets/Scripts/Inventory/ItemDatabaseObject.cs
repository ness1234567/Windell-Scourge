using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDatabaseObject : MonoBehaviour
{
    [SerializeField]
    private ItemDatabase itemsDatabase;
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

    public Item getItembyID(int id)
    {
        return itemsDatabase.ItemList[id];
    }

    public Item getItembyName(string name)
    {
        return itemsDatabase.ItemList.FirstOrDefault(i => string.Equals(i.item_name, name));
    }

    public Item getRandomItem()
    {
        return itemsDatabase.ItemList[Random.Range(0, itemsDatabase.ItemList.Count())];
    }

}
