using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonCircle : MonoBehaviour {

    public List<Altar> nodes = new List<Altar>();
    public GameObject summon;
    public bool ritualCompleted = false;
    public Transform[] spawnPoints;
    int sacrificePoints = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (AllFilled() && !ritualCompleted)
        {
            Summon();
            ritualCompleted = true;
        }
            
	}

    public void Summon()
    {
        //check if all circles are filled
        if (AllFilled() && !ritualCompleted)
        {
            //kill everything on the nodes
            foreach (var node in nodes)
            {
                sacrificePoints += node.SacrificePoints;
                node.SacrificePoints = 0;
            }
            //spawn demon
            foreach(Transform spawn in spawnPoints)
            {
                GameObject.Instantiate(summon, spawn.position, Quaternion.identity);
            }
                
            //increment rituals by one
            //GameManager.instance.Rituals++;
            //GameManager.instance.sacrificePoints += sacrificePoints;
            //GameManager.instance.CheckGameOver();
        }
    }

    bool AllFilled()
    {
        bool allFilled = true;
        foreach (var node in nodes)
        {
            if (!node.filled)
                allFilled = false;
        }
        return allFilled;
    }

}
