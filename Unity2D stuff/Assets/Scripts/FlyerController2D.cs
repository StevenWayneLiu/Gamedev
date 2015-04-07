using UnityEngine;
using System.Collections;

public class FlyerController2D : MonoBehaviour {

    //movement variables
    public float maxSpeed = 5f;
    public float xDir;
    public float yDir;
    public float yVel;
    public float xVel;
    private bool fire = false;
    private float angle;

    //bullet buffer stuff
    public GameObject bullet;
    private BulletPool bullets;//bullet buffer array, holds bullets inside

    // Use this for initialization
    void Start()
    {
        xVel = 0f;
        yVel = 0f;
        angle = 0f;
        bullets = gameObject.GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input from controls
        fire = Input.GetButton("Fire1");
        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");

        //find out angle of movement vector for figuring out magnitude of movement vector
        if (xDir == 0)//if not moving on the x direction
            angle = (Mathf.PI / 2);//set the angle to pi/2
        else
            angle = Mathf.Atan(Mathf.Abs(yDir / xDir));//else, calculate angle for movement

        if (fire)//if firing and the cooldown is over, then fire
        {
            bullets.Fire();
        }
    }
    void FixedUpdate()
    {
        //ensure magnitude of total velocity vector doesn't exceed max speed
        xVel = Mathf.Clamp(Input.GetAxis("Horizontal") * maxSpeed, -1 * maxSpeed * Mathf.Cos(angle), maxSpeed * Mathf.Cos(angle));
        yVel = Mathf.Clamp(Input.GetAxis("Vertical") * maxSpeed, -1 * maxSpeed * Mathf.Sin(angle), maxSpeed * Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }
}
