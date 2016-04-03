using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class Actor : MonoBehaviour
{
    public Attributes stats = new Attributes();
	public float moveTime = 0.1f;			        //Time it will take object to move, in seconds.
	protected Rigidbody rb;				            //The Rigidbody2D component attached to this object.
	private float inverseMoveTime;			        //Used to make movement more efficient.
    protected Vector2 gridPos = new Vector2(0, 0);  //index on grid that this character is standing at
    public Vector2 dir;                             //Direction character is facing
    public Vector2 nextTile;                            //Tile that unit is attempting to move to
    protected bool isMoving = false;
    protected Animator animator;

    //references to global objects
    public TerrainManager tm = TerrainManager.instance;
		
	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start ()
	{
			
		//Get a component reference to this object's Rigidbody2D
		rb = GetComponent <Rigidbody> ();

        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();

		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		inverseMoveTime = 1f / moveTime;

	}
	
	protected bool Move (Vector2 dir)
	{
		//multiply new coordinate position by unity units per tile
		Vector2 dest = gridPos + dir;

        nextTile = dest;
        //check if destination is an invalid tile coordinate
        if (!TerrainManager.instance.isInBounds(dest))
        {
            Debug.Log("Invalid move destination");
            return false;
        }
        else//valid tile coordinate
        {
            if (!tm.gridContents.ContainsKey(dest))//add new list for position if there isn't already one
                tm.gridContents.Add(dest, new List<Actor>());
            tm.gridContents[dest].Add(this);
            isMoving = true;
            StartCoroutine(SmoothMovement(tm.WorldPos(dest)));
            tm.gridContents[gridPos].Remove(this);
            gridPos = dest;
            
            //Return true to say that Move was successful
            return true;
        }
	}
		
	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		
		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while(sqrRemainingDistance > float.Epsilon)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
				
			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb.MovePosition (newPosition);
				
			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
				
			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
			yield return null;
		}
        isMoving = false;
	}

    //path-finding functionality for units
    public void PathFind (Vector2 dest)
    {

    }
    //returns the area of a skill, transformed to fit the direction the actor is facing
    public Vector2[] TransformArea(Vector2[] orig)
    {
        Vector2[] newArea = new Vector2[orig.Length];
        for (int i = 0; i < newArea.Length; i++)//apply effect to all grids in area
        {
            //area position in respect to actor's direction
            Vector2 dirArea;
            if (dir.x != 0)
            {
                dirArea = new Vector2(orig[i].y * dir.x, orig[i].x);
            }
            else
            {
                dirArea = new Vector2(orig[i].x, orig[i].y * dir.y);
            }
            newArea[i] = dirArea;
        }
        return newArea;
    }

    public void Attack(Skill sk)
    {
        Vector2[] area = TransformArea(sk.area);
        for (int i = 0; i < area.Length; i++)//apply effect to all grids in area
        {
            Vector2 pos = gridPos + area[i];
            if (tm.isInBounds(pos) && tm.gridContents[pos] != null)
            { 
                //attack entities at location
                foreach(Actor actor in tm.gridContents[pos])
                    actor.stats.health -= stats.strength;
            }
        }
    }

    public void changeTile(Skill sk, int type)
    {
        tm.editTerrain(TransformArea(sk.area), type);
    }
}

