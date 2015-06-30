using UnityEngine;
using System.Collections;

public class mockup1enemycontroller : MonoBehaviour {

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
