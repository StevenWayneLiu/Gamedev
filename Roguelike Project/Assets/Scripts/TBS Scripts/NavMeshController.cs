using UnityEngine;
using System.Collections;

public class NavMeshController : MonoBehaviour {

    NavMeshAgent agent;

    float velX = 0;//horizontal velocity
    float velZ = 0;//vertical velocity
    Vector3 dir;//direction to apply acceleration in
    Vector3 velocity = Vector3.zero;//current velocity vector
    float acceleration = 5f;
    float moveVel = 5f;//maximum movement speed
	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Move();
	}

    void FixedUpdate()
    {
        
    }

    //moves the character to the point on the screen where the mouse is
    public void Move()
    {
        /*RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);//cast a ray where mouse clicked
        agent.SetDestination(hit.point);//set destination to point of collision with map
        Debug.Log(hit.point);
        */
        velX = Input.GetAxisRaw("Horizontal");
        velZ = Input.GetAxisRaw("Vertical");
        
        if((velX == 0) && (velZ == 0))//if input is zero
        {
            Debug.Log("Decelerating");
            dir = -velocity.normalized;//get opposite direction to velocity vector
            dir *= acceleration * Time.deltaTime;
            dir = Vector3.ClampMagnitude(dir, velocity.magnitude);//clamp magnitude to current velocity vector's magnitude
        }
        else
        {
            Debug.Log("Accelerating");
            dir = new Vector3(velX, 0, velZ).normalized;//direction corresponds to input
            dir *= acceleration * Time.deltaTime;//acceleration vector
        }

        velocity += dir;//add acceleration to velocity
        velocity = Vector3.ClampMagnitude(velocity, moveVel * Time.deltaTime);//clamp the velocity to max speed
        Debug.Log(velocity/Time.deltaTime);
        agent.Move(velocity);//move agent
    }
        
}
