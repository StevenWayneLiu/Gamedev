using UnityEngine;
using System.Collections;

public class BattlerController : MonoBehaviour {

    public CharacterBaseClass charData;
    public float maxSpeed = 5f;
    public float xDir;
    public float yDir;
    public float yVel;
    public float xVel;
    private float angle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");

        //find out angle of movement vector for figuring out magnitude of movement vector
        if (xDir == 0)//if not moving on the x direction
            angle = (Mathf.PI / 2);//set the angle to pi/2
        else
            angle = Mathf.Atan(Mathf.Abs(yDir / xDir));//else, calculate angle for movement
	}

    void FixedUpdate()
    {
        if (charData == GameStateManager.stateManager.curChar)//only move when it's player's turn and they're moving
        {
            //ensure magnitude of total velocity vector doesn't exceed max speed
            xVel = Mathf.Clamp(Input.GetAxis("Horizontal") * maxSpeed, -1 * maxSpeed * Mathf.Cos(angle), maxSpeed * Mathf.Cos(angle));
            yVel = Mathf.Clamp(Input.GetAxis("Vertical") * maxSpeed, -1 * maxSpeed * Mathf.Sin(angle), maxSpeed * Mathf.Sin(angle));
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
        }
    }
    void OnMouseDown()
    {
        GameStateManager.stateManager.target = charData;//select the character associated to this object
        GameStateManager.stateManager.targetObject = gameObject;//select this object
    }
}
