using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindNPC : MonoBehaviour {

    public List<GameObject> entities;

	// Use this for initialization
	void Start () {
        entities = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && returnClosest() != null && returnClosest().GetComponent<NPCInteraction>() != null)
        {
            //if space bar is pressed, get nearest npc and activate their interaction behavior
            returnClosest().GetComponent<NPCInteraction>().Interact();
        }
	}

    //return the closest NPC in proximity
    public GameObject returnClosest()
    {
        GameObject targ = null;
        float dist = 100;//arbitrary number
        for (int i = 0; i < entities.Count; i++)
        {
            if(Vector2.Distance(gameObject.transform.position,entities[i].transform.position) < dist)
            {
                targ = entities[i];
            }
        }

        return targ;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (other.GetComponent<CharacterManager>() != null && other.tag == "NPC")
        {
            entities.Add(other);
        }
    }
}
