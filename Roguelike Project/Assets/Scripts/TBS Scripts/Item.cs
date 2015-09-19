using UnityEngine;
using System.Collections;

public class Item {

    public int index;//item index in global inventory
	public string name;
    public int stackCap = 1;//the amount of this item that can be stacked
    public Vector3 userDest;//transform to move the user to
    public Vector3 targDest;//transform to move the target to
}
