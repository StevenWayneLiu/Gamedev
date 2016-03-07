using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Altar : MonoBehaviour {

    public int cost = 3;//sacrifice cost for this
    int currValue = 0;//value of all things currently on the circle
    public bool filled = false;
    public bool active = true;
    public SummonCircle circle;//parent circle of this node
    public List<Entity> targets = new List<Entity>();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
    //sacrifice all targets on the node
    public void Sacrifice()
    {
    }

    public void OnTriggerEnter(Collider other)
    { 
    }
    public void OnTriggerExit(Collider other)
    {
    }

    public int SacrificePoints { get { return currValue; } set { currValue = value; } }
}
