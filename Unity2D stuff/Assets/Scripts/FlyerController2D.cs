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
    public GameObject bulletPrefab;
    private GameObject[] bullets;//bullet buffer array, holds bullets inside
    private int bufferIndex = 0;//current index of bullet array that is being acted on
    private int bufferCap = 20;//the maximum number of bullets that can be on the screen at once
    private Vector3 fireLoc;

    //cooldown and fire rate
    private float cooldown = 0.2f;//time it takes to be able to fire a bullet again when button is held down
    public float fireRate;
    private float cooldownTimer = 0f;//time until player is able to fire again while holding fire button
    private Vector3 bufferLoc = new Vector3(0f, 0f, 0f);//location of the bullet buffer

    // Use this for initialization
    void Start()
    {
        xVel = 0f;
        yVel = 0f;
        angle = 0f;
        fireRate = (1 / cooldown);//reflects how many shots can be fired in one second, rounded down
        cooldownTimer = cooldown;//set timer to cooldown so player can fire immediately
        bullets = new GameObject[bufferCap];
        for (int index = 0; index < bufferCap; index++)//fills the buffer with bullets to use
        {
            bullets[index] = (GameObject)GameObject.Instantiate(bulletPrefab, bufferLoc, gameObject.transform.rotation);
            bullets[index].GetComponent<BulletScript>().setFired(false,bufferLoc);//turn off bullet movement and send it to buffer
        }
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

        //cooldown timer for firing shots
        cooldownTimer += Time.deltaTime;//set a cooldown timer to count up from zero for number of seconds 
        if (fire && cooldownTimer >= cooldown)//if firing and the cooldown is over, then fire
        {
            if (bufferIndex == bufferCap)//if at buffer cap, need to go back to index zero
            {
                bufferIndex = 0;//resets index to start at beginning of array
            }
            fireLoc = gameObject.transform.position;//set position to fire from to the player position
            bullets[bufferIndex].GetComponent<BulletScript>().setFired(true,fireLoc);//activates bullet and moves it to firing location

            cooldownTimer = 0f;//reset timer
            bufferIndex++;
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
