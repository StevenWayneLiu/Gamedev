using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {
	
	//Horizontal movement
	public float maxSpeed = 10f;
	public float curSpeed = 0f;
	private float gravity = 1f;//downwards acceleration rate
	public float accel = 2;//horizontal acceleration rate
	public float targetSpeed;
	public float dir;//direction of horizontal axis input
	public bool grounded = false;
	
	//Vertical movement
	public bool jump = false;
	public float maxFallSpeed = -10f;
	public bool isFalling;//used to detect if character is going up or down, for platform collision
	public float jumpStrength = 50f;//upwards instantaneous acceleration rate
	public float fall=0.0f;//vertical velocity

	
	public Vector2 speed;
	
	//Raycast
	private Vector2 top;
	private Vector2 left;
	private Vector2 right;
	private Vector2 bottom;
	private RaycastHit2D[,] raycasts = new RaycastHit2D[4,2];//stores raycasthits
	private float edgeOffset = 0.01f;//offset of raycasts from the edge of the collider
	
	
	//Distance measurements
	private BoxCollider2D hitBox;
	public float halfHeight;//half the height of character
	public float halfWidth;//half the width of character

	//raycast distances
	public float upDist;//distance from origin to ceiling
	public float downDist;//height from origin to floor
	public float leftDist;//distance from origin to the left
	public float rightDist;//distance from origin to the right

	
	// Use this for initialization
	void Start () {
		hitBox = gameObject.GetComponent<BoxCollider2D>();

		halfHeight = hitBox.size.y/2.0f;
		halfHeight -= halfHeight%0.001f;//round to 3 decimal places for less precision

		halfWidth = hitBox.size.x / 2.0f;
		halfWidth -= halfWidth % 0.001f;
	}

	//Takes in raycasthits, and determines the minimum distance from the two
	float RaycastSide(RaycastHit2D left, RaycastHit2D right){
		float minDist;
		if(!left && !right)
			minDist = Mathf.Infinity;
		else{//else find the left distance
			if(left && right){//if both rays hit a collider
				if(left.distance<right.distance){//set left distance to the distance of the raycast closest to a surface.
					minDist = left.distance;
				}
				else{
					minDist = right.distance;
				}
			}
			else//if one of the rays hits no collider, then measure using the one that does.
				if(left){
					minDist = left.distance;
				}
				else{
					minDist = right.distance;
				}
		}
		return minDist;
	}
	
	// Update is called once per frame

	void Update(){

		//get input
		dir = Input.GetAxisRaw("Horizontal");
		jump = Input.GetButtonDown("Jump");
		
		//raycast origin points
		top = new Vector2(transform.position.x,transform.position.y+(hitBox.size.y/2f-edgeOffset));
		left = new Vector2(transform.position.x-(hitBox.size.x/2f-edgeOffset),transform.position.y);
		right = new Vector2(transform.position.x+(hitBox.size.x/2f-edgeOffset),transform.position.y);
		bottom = new Vector2(transform.position.x,transform.position.y-(hitBox.size.y/2f-edgeOffset));

		int layerMask = 1<<8;
		layerMask = ~layerMask;//ignore layer 8
		
		//down raycasts
		raycasts[0,0] = Physics2D.Raycast(left,-Vector2.up,Mathf.Infinity,layerMask);
		raycasts[0,1] = Physics2D.Raycast(right,-Vector2.up,Mathf.Infinity,layerMask);
		//right raycasts
		raycasts[1,0] = Physics2D.Raycast(top,Vector2.right,Mathf.Infinity,layerMask);
		raycasts[1,1] = Physics2D.Raycast(bottom,Vector2.right,Mathf.Infinity,layerMask);
		//up raycasts
		raycasts[2,0] = Physics2D.Raycast(left,Vector2.up,Mathf.Infinity,layerMask);
		raycasts[2,1] = Physics2D.Raycast(right,Vector2.up,Mathf.Infinity,layerMask);
		//left raycasts
		raycasts[3,0] = Physics2D.Raycast(top,-Vector2.right,Mathf.Infinity,layerMask);
		raycasts[3,1] = Physics2D.Raycast(bottom,-Vector2.right,Mathf.Infinity,layerMask);

		//raycasthit2ds
		upDist = RaycastSide (raycasts [2,0], raycasts [2,1]);
		downDist = RaycastSide (raycasts [0,0], raycasts [0,1]);
		rightDist = RaycastSide (raycasts [1,0], raycasts [1,1]);
		leftDist = RaycastSide (raycasts [3,0], raycasts [3,1]);

		//locomotion
		targetSpeed = maxSpeed*dir;
		if(curSpeed!=targetSpeed){//if not at target speed
			if(targetSpeed>curSpeed){//if target speed is greater, accelerate positively
				curSpeed+=accel;
			}
			else{
				curSpeed-=accel;//if target speed is less, accelerate negatively
			}
		}

		float approxHeight = downDist - downDist%0.001f;
		//set grounded
		if(approxHeight<=halfHeight && (raycasts[0,0].collider || raycasts[0,1].collider)){//if height is equal to or less than standing height, counts as on the ground
			grounded = true;
		}
		else{
			grounded = false;
		}

		//fall acceleration
		if(!grounded){//if character is in the air
			if(fall>=maxFallSpeed){//if the fall speed is less than or equal to max fall speed
				fall -= gravity;//accelerate downward
			}
		}
		else{//if character is on the ground
			fall = 0.0f;//stop falling velocity
			if(jump){
				fall=jumpStrength;
			}
		}
		
		//move player
		speed = new Vector2(Mathf.Clamp(curSpeed*Time.deltaTime,-leftDist+halfWidth,rightDist-halfWidth),Mathf.Clamp(fall*Time.deltaTime,-downDist+halfHeight,upDist-halfHeight));
		transform.Translate(speed);//update position with speed vector
		
	}
}
