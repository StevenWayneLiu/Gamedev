using UnityEngine;
using System.Collections;

public class BackupDancerController : MonoBehaviour {

    //components
    private Animator animator;
    private CharacterController controller;

    //input variables
    private float Xvel;//horizontal input
    private float Zvel;//vertical input
    private bool attack = false;//input for attack

    //parameters
    public float maxSpeed = 0.04f;//movement speed in units per second
    public float rotSpeed = 720f;//rotation speed in degrees per second
    private Vector3 curVelocity;//vector storing current velocity of character
    public float acceleration = 5f;//movement acceleration in units per second
    public float termFall = 0.5f;//terminal falling velocity

    //variables for calculations
    private Vector3 targetDir;//target direction of movement
    float rotDir;//polarity for rotation angle; whether the character is rotating clockwise or counterclockwise

    private bool isMoving = false;

    //set up the animation stuff
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        curVelocity = Vector3.zero;//current velocity should be zero
    }

    //do movement and input in here
    void Update()
    {
        //get input from player
        Xvel = Input.GetAxisRaw("Horizontal");
        Zvel = Input.GetAxisRaw("Vertical");
        attack = Input.GetButtonDown("Fire1");

        //add acceleration to current velocity vector
        if (curVelocity != targetDir * maxSpeed)//if the current velocity isn't the target velocity
        {
            if (targetDir == Vector3.zero)//if the target direction is zero
                curVelocity -= Vector3.ClampMagnitude(curVelocity, acceleration * Time.deltaTime);//start diminishing the current velocity vector, capping the acceleration
            else//else if the target direction is not zero
                curVelocity += targetDir * acceleration * Time.deltaTime;//add acceleration in direction of target velocity
            curVelocity = Vector3.ClampMagnitude(curVelocity, maxSpeed);//clamp velocity vector to the maximum speed
        }

        //rotate in direction of movement, not in target direction
        if (targetDir != Vector3.zero)//only change rotation if character is accelerating towards a nonzero direction
        {
            Rotate(curVelocity);//rotate it in the direction of motion
            //transform.rotation = Quaternion.LookRotation(curVelocity,transform.up);
        }

        //add gravity at the end
        //raycast downwards to check for floor
        RaycastHit downcheck;
        Physics.Raycast(transform.position, Vector3.down, out downcheck);

        //move to the ground clamped by the fallspeed
        if (downcheck.distance > 1.2f)//only move downwards if not on the ground
            curVelocity -= Vector3.up * Mathf.Clamp(termFall, 0f, downcheck.distance + 0.1f);

        //translation
        controller.Move(curVelocity);
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

        //attack animation
        animator.SetBool("attack", attack);
    }

    //rotates the transform towards the target direction Vector3
    void Rotate(Vector3 targetDir)
    {
        if (Vector3.Cross(transform.forward, targetDir.normalized).y < 0)//use cross product of the starting and ending rotation positions to determine direction to rotate
            rotDir = -1;
        else
            rotDir = 1;
        //rotate character to new position, clamping rotation amount by the rotationspeed to smooth it
        transform.Rotate(new Vector3(0, Mathf.Clamp(Vector3.Angle(transform.forward, targetDir) * rotDir, -rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime), 0));
    }
}
