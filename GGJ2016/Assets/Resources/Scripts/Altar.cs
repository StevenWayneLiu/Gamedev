using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Altar : MonoBehaviour {

    public int cost = 3;//sacrifice cost for this
    int currValue = 0;//value of all things currently on the circle
    public bool filled = false;
    public bool active = true;
    public SummonCircle circle;//parent circle of this node
    public List<Unit> targets = new List<Unit>();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
    //sacrifice all targets on the node
    public void Sacrifice()
    {
        foreach (Unit fol in targets)
        {
            fol.Death();
        }
        active = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.GetComponent<Unit>() != null)
            {
                Unit targ = other.GetComponent<Unit>();
                currValue += targ.value;
                targets.Add(targ);
            }
            if (currValue >= cost)
            {
                filled = true;
                Sacrifice();
            }
        }  
    }
    public void OnTriggerExit(Collider other)
    {
        if (active)
        {
            //if target leaves, take them off the list for sacrifice
            if (other.GetComponent<Unit>() != null)
            {
                Unit targ = other.GetComponent<Unit>();
                currValue -= targ.value;
                targets.Remove(targ);
            }
            if (currValue < cost)
                filled = false;
        }
    }

    public int SacrificePoints { get { return currValue; } set { currValue = value; } }
}
