using UnityEngine;


public class TerrainData 
{
	public AudioClip chopSound1;				//1 of 2 audio clips that play when the wall is attacked by the player.
	public AudioClip chopSound2;				//2 of 2 audio clips that play when the wall is attacked by the player.
    public int spriteSheet = 0;                     //index of sprite sheet in global sprite sheet list
    public Vector2 normUV;                  //UV coordinates for bottom left corner of normal texture
    public Vector2 normUV2;                 //upper right corner
    public Vector2 dmgUV;                   //UV coordinates for damaged texture
    public Vector2 dmgUV2;
    public int health = 3;							//hit points for the wall.
    public bool isSolid = false;

    public TerrainData()
    {
        normUV = new Vector2(0, 0);
        dmgUV = new Vector2(0, 0);
    }

    public TerrainData(Vector2 uv1, Vector2 uv2, int hp)
    {
        
        normUV = uv1;
        normUV2 = uv2;
        health = hp;
    }
		
	//DamageWall is called when the player attacks a wall.
	public void DamageWall (int loss)
	{
		//Call the RandomizeSfx function of SoundManager to play one of two chop sounds.
		SoundManager.instance.RandomizeSfx (chopSound1, chopSound2);
			
		//Set spriteRenderer to the damaged wall sprite.
			
		//Subtract loss from hit point total.
		health -= loss;
			
		//If hit points are less than or equal to zero:
        if (health <= 0) { }
			//Disable the gameObject.
	}
}

