﻿using UnityEngine;
using System.Collections;

public class NavMeshController : MonoBehaviour {

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
            agent.SetDestination(hit.point);//set destination to point of collision with map
            Debug.Log(hit.point);
        }
	}

    void FixedUpdate()
    {

    }


        
}
