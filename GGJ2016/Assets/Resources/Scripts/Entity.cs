using UnityEngine;
using System.Collections;

namespace Completed
{
	//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
	public abstract class Entity : MonoBehaviour
	{
        public Attributes stats = new Attributes();
		public float moveTime = 0.1f;			//Time it will take object to move, in seconds.
		public LayerMask blockingLayer;			//Layer on which collision will be checked.
		
		private BoxCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
		private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.
		private float inverseMoveTime;			//Used to make movement more efficient.
        protected Vector2 gridPos = new Vector2(1, 1);//index on grid that this character is standing at
        public Vector2 dir;                     //Direction character is facing
		
		//Protected, virtual functions can be overridden by inheriting classes.
		protected virtual void Start ()
		{
			//Get a component reference to this object's BoxCollider2D
			boxCollider = GetComponent <BoxCollider2D> ();
			
			//Get a component reference to this object's Rigidbody2D
			rb2D = GetComponent <Rigidbody2D> ();
			
			//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
			inverseMoveTime = 1f / moveTime;

		}
		
		
		//Move returns true if it is able to move and false if not. 
		//Move takes parameters for x direction, y direction.
		protected bool Move (int xDir, int yDir)
		{
			//multiply new coordinate position by unity units per tile
			Vector2 dest = gridPos + new Vector2(xDir, yDir);
			
            //check if destination is an invalid tile coordinate
            if (dest.x >= BoardManager.instance.columns || dest.y >= BoardManager.instance.rows || dest.x < 0 || dest.y < 0)
            {
                return false;
            }
            else//valid tile coordinate
            {
                StartCoroutine(SmoothMovement(BoardManager.instance.WorldPos(dest)));
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
				Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
				
				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				rb2D.MovePosition (newPosition);
				
				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (transform.position - end).sqrMagnitude;
				
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
			}
		}
		
		
		//The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
		//AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
		protected virtual void AttemptMove (int xDir, int yDir)
		{
            dir = new Vector2(xDir, yDir);
			if(Move(xDir, yDir))
            {
                
            }
            else
                OnCantMove();
		}

        protected void OnCantMove()
        {
            //Play a sound effect or something when a unit is blocked
        }

        public void Attack(Skill sk)
        {
            Vector2[] area = sk.area;
            for (int i = 0; i < area.Length; i++)//apply effect to all grids in area
            {
                //area position in respect to entity's direction
                Vector2 dirArea;
                if (dir.x != 0)
                {
                    dirArea = new Vector2(area[i].y * dir.x, area[i].x);
                }
                else
                {
                    dirArea = new Vector2(area[i].x, area[i].y * dir.y);
                }
                Vector2 pos = gridPos + area[i];
                if (BoardManager.instance.grid[(int)pos.x, (int)pos.y] != null && GameManager.instance.entities[pos] != null)
                { 
                    //attack entity at location
                    GameManager.instance.entities[pos].stats.health -= stats.strength;
                }
            }
        }
	}
}
