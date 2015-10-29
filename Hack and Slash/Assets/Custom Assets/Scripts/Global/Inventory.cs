using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

    List<Item> items;

    public Inventory()
    {

    }

    public void AddItem(Item i)
    {
        items.Add(i);
    }
    //remove a value at a specified index
    public void RemoveItem(int i)
    {
        items.RemoveAt(i);
    }

    public Item GetItem(int index)
    {
        return items[index];
    }
	
}


