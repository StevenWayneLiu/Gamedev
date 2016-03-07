using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public List<Unit> units = new List<Unit>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Unit this[int i]
    {
        get { return units[i]; }
        set { units[i] = value; }
    }

    void GroupMove(Vector3 newPos)
    {
        foreach (var unit in units)
        {
            unit.Move(newPos + UnitOffset(unit));
        }
    }

    //find a unit's offset from the center of the group
    Vector3 UnitOffset(Unit unit)
    {
        Vector3 offset;
        offset = unit.transform.position - transform.position;
        return offset;
    }

}
