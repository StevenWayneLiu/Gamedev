using UnityEngine;
using System.Collections;
using System.Collections.Generic;		//Allows us to use Lists. 
using UnityEngine.UI;					//Allows us to use UI.
	
public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;				                    //Static instance of GameManager which allows it to be accessed by any other script.
    public List<Actor> allActors = new List<Actor>();                        //list of all actors in game
    public List<TerrainData> terrainTypes = new List<TerrainData>();            //List of types of tiles
    public List<Texture> spriteSheets = new List<Texture>();

    //references to global objects
    public TerrainManager tm = TerrainManager.instance;
	
	//Awake is always called before any Start functions
	void Awake()
	{
        //singleton pattern
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	

		DontDestroyOnLoad(gameObject);
		

        terrainTypes = new List<TerrainData>();
	}

    public void AddActor(Actor newActor, Vector2 pos)
    {
        allActors.Add(newActor);
        if (!tm.gridContents.ContainsKey(pos))//add new list for position if there isn't already one
            tm.gridContents.Add(pos, new List<Actor>());
        tm.gridContents[pos].Add(newActor);
    }
    public void RemoveActor(Actor actor, Vector2 pos)
    {
        if (!allActors.Contains(actor))
            return;
        else
        {
            allActors.Remove(actor);
            tm.gridContents[pos].Remove(actor);
        }
    }

	//Update is called every frame.
	void Update()
	{
	}
		
}

