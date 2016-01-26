using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Inventory <T>{

    [SerializeField]public List<T> entities;
    //maybe have a dictionary for names that corresponds to index in entities list

    public Inventory()
    {
        entities = new List<T>();
    }

    public Inventory(Inventory<T> orig)
    {
        entities = new List<T>(orig.entities);//clone entities
    }

    //square bracket access to entities list
    public T this[int index]
    {
        get
        {
            return entities[index];
        }
        set
        {
            entities[index] = value;
        }
    }

    //properties
    public int Count
    {
        get { return entities.Count; }
    }

    //load item list
    public void Load()
    {

    }

    //get function
    public T GetEntity(int index)
    {
        return entities[index];
    }

    public bool Contains(T en)
    {
        if (entities.Contains(en))
            return true;
        else
            return false;
    }

	//sort function
    public void Sort()
    {

    }

    //add and sort
    public void AddItem(T i)
    {
        entities.Add(i);
    }
    //remove and sort item
    public void RemoveItem(int i)
    {
        entities.RemoveAt(i);
    }

    //function for trading entities to other inventories
    public void MoveEntry(Inventory<T> targ, int index)
    {
        targ.AddItem(entities[index]);
        entities.RemoveAt(index);
    }

    //function for copying item to another inventory
    public void CopyEntry(Inventory<T> targ, int index)
    {
        //targ.AddItem(new T(entities[index]));
    }
}


