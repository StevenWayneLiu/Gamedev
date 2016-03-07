using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public List<Entity> units = new List<Entity>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Entity this[int i]
    {
        get { return units[i]; }
        set { units[i] = value; }
    }

    void GroupMove(Vector3 newPos)
    {
        foreach (var unit in units)
        {
            //move unit
        }
    }

    //find a unit's offset from the center of the group
    Vector3 UnitOffset(Entity unit)
    {
        Vector3 offset;
        offset = unit.transform.position - transform.position;
        return offset;
    }

}
