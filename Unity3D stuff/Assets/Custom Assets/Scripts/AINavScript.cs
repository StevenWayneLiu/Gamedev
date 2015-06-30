using UnityEngine;
using System.Collections;

public class AINavScript : MonoBehaviour {

    private Animator animator;
    public Transform target;
    NavMeshAgent agent;
    Vector3 curVelocity;
    private bool isMoving = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
        curVelocity = agent.velocity;
	}

    //do animations in here
    void LateUpdate()
    {
        //update the animator afterwards
        if (Vector3.Scale(curVelocity, new Vector3(1, 0, 1)) == Vector3.zero)
            isMoving = false;
        else
            isMoving = true;
        animator.SetBool("isMoving", isMoving);

    }
}
