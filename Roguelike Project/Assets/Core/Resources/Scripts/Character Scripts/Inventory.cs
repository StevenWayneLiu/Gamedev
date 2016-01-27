using UnityEngine;
using System.Collections;

[System.Serializable]
public class Inventory : Database<ItemData> {

    public Inventory()
        : base()
    {

    }
    public Inventory(Inventory orig)
        : base(orig)
    {

    }

    public override ItemData CopyEntry(int index)
    {
        return new ItemData(entries[index]);
    }
}
