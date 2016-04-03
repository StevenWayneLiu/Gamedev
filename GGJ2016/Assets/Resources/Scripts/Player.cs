using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;


//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : Actor
{
						//Used to store a reference to the Player's animator component.

	//Start overrides the Start function of MovingObject
	protected override void Start ()
	{
        base.Start();

        //spawn player at position 0,0 on the grid
        rb.MovePosition(TerrainManager.instance.WorldPos(Vector2.zero));

        dir = Vector2.zero;
	}
		
	private void FixedUpdate ()
	{
		//don't allow vertical movement
        dir = new Vector2((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        if ((int)Input.GetAxisRaw("Horizontal") != 0)
            dir.y = 0;

        if (Input.GetButton("Jump"))
        {
            Attack(new Skill());
        }

		//Check if we have a non-zero value for horizontal or vertical
		if(dir != Vector2.zero && !isMoving)
		{
            Debug.Log(dir.x + " " + dir.y);
			Move(dir);
		}
	}


		

}


