using UnityEngine;
using System.Collections;


public class mockup1controller : MonoBehaviour {

    //camera info
    public Transform camera;
    private Vector3 camForward;//forward direction for the camera, minus y axis
    private Vector3 camRight;//right direction for the camera, minus y axis

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

        //make directional input relative to the camera
        targetDir = CameraRelativeMove(camera, Xvel, Zvel);

        //add acceleration to current velocity vector
        if (curVelocity != targetDir * maxSpeed)//if the current velocity isn't the target velocity
        {
            if(targetDir == Vector3.zero)//if the target direction is zero
                curVelocity -= Vector3.ClampMagnitude(curVelocity, acceleration * Time.deltaTime);//start diminishing the current velocity vector, capping the acceleration
            else//else if the target direction is not zero
                curVelocity += targetDir * acceleration * Time.deltaTime;//add acceleration in direction of target velocity
            curVelocity = Vector3.ClampMagnitude(curVelocity, maxSpeed);//clamp velocity vector to the maximum speed
        }

        //rotate in direction of movement, not in target direction
        if(targetDir != Vector3.zero)//only change rotation if character is accelerating towards a nonzero direction
        {
            Rotate(curVelocity);//rotate it in the direction of motion
            //transform.rotation = Quaternion.LookRotation(curVelocity,transform.up);
        }

        //add gravity at the end
        //raycast downwards to check for floor
        RaycastHit downcheck;
        Physics.Raycast(transform.position, Vector3.down, out downcheck);

        //move to the ground clamped by the fallspeed
        if(downcheck.distance > 0.15f)//only move downwards if not on the ground
            curVelocity -= Vector3.up * Mathf.Clamp( termFall, 0f, downcheck.distance + 0.1f);

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

    //take in a camera transform and horizontal and vertical directional input, output the camera-relative movement direction.
    private Vector3 CameraRelativeMove(Transform camera, float h, float v)
    {
        Vector3 camForward;
        Vector3 camRight;
        if (camera.eulerAngles.x == 90)//if camera's looking straight down
            camForward = Vector3.Scale(camera.up, new Vector3(1, 0, 1));//change camera's up vector into world coordinates
        else if (camera.eulerAngles.x == 270)//else if camera's looking straight up
            camForward = -Vector3.Scale(camera.up, new Vector3(1, 0, 1));//change camera's negative up vector to world coordinates
        else//otherwise normal case
            camForward = Vector3.Scale(camera.forward, new Vector3(1, 0, 1));
        camRight = Vector3.Scale(camera.right, new Vector3(1, 0, 1));//get camera's right direction vector, minus the y axis
        //find target direction to move in on XZ plane
        Vector3 targetDir = camForward * v + camRight * h;//adds together directional vectors for horizontal and vertical in relation to the camera
        targetDir = targetDir.normalized;//takes a normalized vector and multiplies it by maxspeed to get the target direction vector
        return targetDir;
    }
}
