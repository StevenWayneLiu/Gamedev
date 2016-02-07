using UnityEngine;
using System.Collections;

public class PhysicsController2D : MonoBehaviour {
	public float maxSpeed = 5f;
	public float accelForce = 10f;
	public float jumpForce = 10f;
	private float dir;
	private bool jump;
	public bool grounded = false;
	public float yVel;
    public float xVel;
    private bool fire = false;
    public GameObject bulletPrefab;
	// Use this for initialization
	void Start () {
		xVel = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	    dir = Input.GetAxis("Horizontal");
		jump = Input.GetButtonDown ("Jump");
        fire = Input.GetButtonDown("Fire1");
        if (fire)
            GameObject.Instantiate(bulletPrefab,gameObject.transform.position + new Vector3(1f,0f,0f), gameObject.transform.rotation);
	}
	void FixedUpdate(){
		yVel = GetComponent<Rigidbody2D>().velocity.y;
		
		xVel = dir*maxSpeed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(xVel,GetComponent<Rigidbody2D>().velocity.y);
		if (jump && grounded) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpForce);
		}
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        grounded = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
