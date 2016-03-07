using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum; 			//Minimum value for our Count class.
		public int maximum; 			//Maximum value for our Count class.
			
			
		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
    public static TerrainManager instance;
	public int columns = 8; 										//Number of columns in our game board.
	public int rows = 8;											//Number of rows in our game board.
    public float tileSize = 1;                                      //number of world units per grid unit
    public int[,] grid;                                            //Holds all floor tiles

    [SerializeField]
    int minRoomSize = 10;
		
        
    public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
	public GameObject exit;											//Prefab to spawn for exit.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	public GameObject[] wallTiles;									//Array of wall prefabs.
	public GameObject[] foodTiles;									//Array of food prefabs.
	public GameObject[] enemyTiles;									//Array of enemy prefabs.
	public GameObject[] outerWallTiles;								//Array of outer tile prefabs.
	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.

    //mesh/collision information
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector3> colVertices = new List<Vector3>();
    int textWidth;                                                  //the fraction of the sprite sheet's height for one tile
    int textHeight;                                                 //the fraction of the sprite sheet's width for one tile
    List<Vector2> toRender = new List<Vector2>();                   //list of items to render each pass

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenMesh();
        InitializeGridArray();
    }

	//Generate the array holding tile data
	void InitializeGridArray ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();

        //create grid array
        grid = new int[columns,rows];
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x,y] = 0;
            }
        }
	}
		
		
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
        InitializeGridArray();
	}
		
		
	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, (columns - 2)*(rows - 2));
			
		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];
			
		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
			
		//Return the randomly selected Vector3 position.
		return randomPosition;
	}
		
    //Generates map features by setting terrain data		
	void GenerateTerrainData (GameObject[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum+1);
			
		//Instantiate objects until the randomly chosen limit objectCount is reached
		for(int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();
				
			//Choose a random tile from tileArray and assign it to tileChoice
				
			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
		}
	}
		
		
	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup ();
			
		//Reset our list of gridpositions.
		InitializeGridArray ();
			
		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		GenerateTerrainData (wallTiles, wallCount.minimum, wallCount.maximum);
			
		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
		GenerateTerrainData (foodTiles, foodCount.minimum, foodCount.maximum);
			
		//Determine number of enemies based on current level number, based on a logarithmic progression
		int enemyCount = (int)Mathf.Log(level, 2f);
			
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		GenerateTerrainData (enemyTiles, enemyCount, enemyCount);
			
		//Instantiate the exit tile in the upper right hand corner of our game board
		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}

    public Vector2 WorldPos(Vector2 gridPos)
    {
        return gridPos * tileSize + new Vector2(tileSize/2f,tileSize/2f);
    }
    //return information of thing on tile at gridPos
    public Entity GetTileContents(Vector2 gridPos)
    {
        return GameManager.instance.entities[gridPos];
    }
    //return tile information for tile at gridPos
    public Wall GetTileInfo(Vector2 gridPos)
    {
        return GameManager.instance.tileTypes[grid[(int)gridPos.x,(int)gridPos.y]];
    }

    //generate mesh for array
    void GenMesh()
    {
        //assign vertices
        for (int z = 0; z <= rows; z++)
        {
            for (int x = 0; x <= columns; x++)
            {
                vertices.Add(new Vector3(x * tileSize, 0 * tileSize, z * tileSize));
            }
        }
        mesh.vertices = vertices.ToArray();

        //assign triangles
        for (int z = 0; z < rows * (columns + 1); z += columns + 1)//iterate rows
        {
            for (int x = 0; x < columns; x++)//iterate columns
            {
                //lower triangle: 0, 3, 1
                triangles.Add(x + z);
                triangles.Add(x + z + columns + 1);
                triangles.Add(x + z + 1);
                //upper triangle: 1, 3, 2
                triangles.Add(x + z + 1);
                triangles.Add(x + z + columns + 1);
                triangles.Add(x + z + columns + 2);
            }
        }

        mesh.triangles = triangles.ToArray();

        //assign normals
        Vector3[] normals = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            normals[i] = -Vector3.forward;
        }
        mesh.normals = normals;

        //assign UVs
        Vector2[] uvs = new Vector2[vertices.Count];
        for (int z = 0; z < rows * (columns + 1); z += columns + 1)//iterate rows
        {
            for (int x = 0; x < columns; x++)//iterate columns
            {
                //lower triangle: 0, 3, 1
                uvs[x + z] = new Vector2(0, 0);
                uvs[x + z + columns + 1] = new Vector2(0, 1);
                uvs[x + z + 1] = new Vector2(1, 0);
                uvs[x + z + columns + 2] = new Vector2(1, 1);
            }
        }
        mesh.uv = uvs;

    }
    void GenCollisionMesh()
    {

    }
    void RenderMap()
    {
        if(toRender.Count > 0)
        {

        }
        else
        {

        }
    }



}

