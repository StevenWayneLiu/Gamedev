using UnityEngine;
using System.Collections;

public class AggroCollider : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        //move towards player when player enters aggro collider
        target = col.gameObject.transform;//set target to transform of gameobject hit
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
