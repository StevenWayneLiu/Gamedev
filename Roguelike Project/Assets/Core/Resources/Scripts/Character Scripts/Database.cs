using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Database <T>{

    [SerializeField]public List<T> entries;
    //maybe have a dictionary for names that corresponds to index in entities list

    public Database()
    {
        entries = new List<T>();
    }

    public Database(Database<T> orig)
    {
        entries = new List<T>(orig.entries);//clone entities
    }

    //square bracket access to entities list
    public T this[int index]
    {
        get
        {
            return entries[index];
        }
        set
        {
            entries[index] = value;
        }
    }

    //properties
    public int Count
    {
        get { return entries.Count; }
    }

    //load item list
    public void Load()
    {

    }

    public bool Contains(T en)
    {
        if (entries.Contains(en))
            return true;
        else
            return false;
    }

	//sort function
    public virtual void Sort()
    {

    }

    //add and sort
    public void AddEntry(T i)
    {
        entries.Add(i);
    }
    //remove and sort item
    public void RemoveEntry(int i)
    {
        entries.RemoveAt(i);
    }

    //function for trading entities to other inventories
    public void MoveEntry(Database<T> targ, int index)
    {
        targ.AddEntry(entries[index]);
        entries.RemoveAt(index);
    }

    public virtual T CopyEntry(int index)
    {
        return entries[index];
    }

}


