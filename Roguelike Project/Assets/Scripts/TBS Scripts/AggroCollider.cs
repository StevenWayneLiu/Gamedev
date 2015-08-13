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

    void OnTriggerStay(Collider col)
    {
        if (GameStateManager.stateManager.state == GameStateManager.GameStates.Peace)
        {
            if (col.gameObject.tag == "Player")
            {
                //move towards player when player enters aggro collider
                target = col.gameObject.transform;//set target to transform of gameobject hit
                gameObject.GetComponentInParent<NavMeshAgent>().SetDestination(target.position);
            }
        }
    }
}
