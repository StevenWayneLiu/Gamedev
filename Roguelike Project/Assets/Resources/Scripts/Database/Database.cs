using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class Database <T> : ScriptableObject {

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

    //load item list
    public void Load()
    {

    }

    public bool Contains(T entry)
    {
        if (entries.Contains(entry))
            return true;
        else
            return false;
    }

	//sort function
    public virtual void Sort()
    {

    }

    public void Add(T entry)
    {
        entries.Add(entry);
        EditorUtility.SetDirty(this);
    }
    public void Insert(int index, T ir)
    {
        entries.Insert(index, ir);
        EditorUtility.SetDirty(this);
    }
    public void Remove(int index)
    {
        entries.RemoveAt(index);
        EditorUtility.SetDirty(this);
    }
    public void Remove(T entry)
    {
        entries.Remove(entry);
        EditorUtility.SetDirty(this);
    }
    public int Count
    {
        get { return entries.Count; }
    }
    //square bracket access to entities list
    public T this[int index]
    {
        get { return entries[index]; }
        set { entries[index] = value; EditorUtility.SetDirty(this); }
    }
    public virtual T CopyEntry(int index)
    {
        T entry = entries[index];
        return entry;
    }
}


