using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public List<Actor> units = new List<Actor>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Actor this[int i]
    {
        get { return units[i]; }
        set { units[i] = value; }
    }

    void GroupMove(Vector3 newPos)
    {
        foreach (var unit in units)
        {
            //find offset of unit from center
            //have unit pathfind to new position
            unit.PathFind(newPos + UnitOffset(unit));
        }
    }

    //find a unit's offset from the center of the group
    Vector3 UnitOffset(Actor unit)
    {
        Vector3 offset;
        offset = unit.transform.position - transform.position;
        return offset;
    }

}
