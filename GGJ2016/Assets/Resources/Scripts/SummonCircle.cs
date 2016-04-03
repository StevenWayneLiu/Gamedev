using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonCircle : MonoBehaviour {

    public List<Altar> altars = new List<Altar>();
    public GameObject summon;
    public bool ritualCompleted = false;
    public Transform[] spawnPoints;
    int tributeCost = 0;                        //sacrifice point cost

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            
	}

    void Summon()
    {
        int cumulativePoints = 0;
        foreach (Altar altar in altars)
        {
            cumulativePoints += altar.currValue;
        }

        if (cumulativePoints >= tributeCost)
        {
            //summon successful
            //apply effects of secondary altars
        }
    }


}
