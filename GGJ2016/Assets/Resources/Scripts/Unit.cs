 using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {


    Rigidbody rbody;
    public int value = 1;
    NavMeshAgent navmAgent;

	// Use this for initialization
	void Awake () {
        rbody = GetComponent<Rigidbody>();
        navmAgent = GetComponent<NavMeshAgent>();
	}

    void Start()
    {
        GameManager.instance.units.Add(this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    public void Move(Vector3 destination)
    {
        navmAgent.SetDestination(destination);
    }

    public void Death()
    {
        //remove from selected
        if (GameManager.instance.selected.Contains(this))
            GameManager.instance.selected.Remove(this);
        if (GameManager.instance.units.Contains(this))
            GameManager.instance.units.Remove(this);
        //play death animation

        //destroy object
        gameObject.SetActive(false);
    }
}
