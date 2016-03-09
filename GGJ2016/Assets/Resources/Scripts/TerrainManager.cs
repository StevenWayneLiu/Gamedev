using UnityEngine;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainManager : MonoBehaviour
{
    public static TerrainManager instance;
	public int columns = 8; 										//Number of columns in our game board.
	public int rows = 8;											//Number of rows in our game board.
    public float tileSize = 1;                                      //number of world units per grid unit
    public int[,] grid;                                            //Holds all floor tiles
	public GameObject exit;											//Prefab to spawn for exit.

    //mesh/collision information
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector3> colVertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
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

        LoadTerrainTypes();
        InitializeGridArray();
        GenMesh();

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

	//Generate the array holding tile data
	void InitializeGridArray ()
	{
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
		
    //Generates map features by setting terrain data		
	void GenerateTerrainData ()
	{
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
    public TerrainData GetTileInfo(Vector2 gridPos)
    {
        return GameManager.instance.terrainTypes[grid[(int)gridPos.x,(int)gridPos.y]];
    }

    //generate mesh for array
    void GenMesh()
    {
        //assign vertices
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                //assign vertices clockwise from bottom left corner
                vertices.Add(new Vector3(x * tileSize, 0, z * tileSize));
                vertices.Add(new Vector3(x * tileSize, 0, (z + 1) * tileSize));
                vertices.Add(new Vector3((x + 1) * tileSize, 0, (z + 1) * tileSize));
                vertices.Add(new Vector3((x + 1) * tileSize, 0, z * tileSize));
            }
        }
        mesh.vertices = vertices.ToArray();

        //assign triangles
        for (int i = 0; i < vertices.Count; i+=4)//iterate rows
        {
                //lower triangle: 0, 1, 3
                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i + 3);
                //upper triangle: 1, 2, 3
                triangles.Add(i + 1);
                triangles.Add(i + 2);
                triangles.Add(i + 3);
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
        for (int z = 0; z < rows; z++)//iterate rows
        {
            for (int x = 0; x < columns; x++)//iterate columns
            {
                //clockwise starting from bottom left
                uvs.Add(new Vector2(0, 64f / 224f));
                uvs.Add(new Vector2(0, 96f / 224f));
                uvs.Add(new Vector2(1f / 8f, 96f / 224f));
                uvs.Add(new Vector2(1f / 8f, 64f / 224f));
            }
        }
        mesh.uv = uvs.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    void GenCollisionMesh()
    {

    }
    //go through all tiles to be re-rendered and render them.
    void RenderMap()
    {
        if(toRender.Count > 0)
        {
            for (int i = 0; i < toRender.Count; i++)
            {
                //find index of grid square in vertex/uv list
                int place = (int)toRender[i].y * columns + (int)toRender[i].x;
                //find the terrain type at grid location
                int type = grid[(int)toRender[i].x, (int)toRender[i].y];
                //find the correct tile on sprite sheet for terrain type
                Vector2 uv = GameManager.instance.terrainTypes[type].normUV;
                uvs[place] = uv;
                uvs[place] = uv + Vector2.up;
                uvs[place] = uv + Vector2.one;
                uvs[place] = uv + Vector2.right;
            }
        }
        mesh.uv = uvs.ToArray();
    }

    //Load in all terrain types into the game data
    void LoadTerrainTypes()
    {
        if (GameManager.instance.spriteSheets.Count == 0)
        {
            GameManager.instance.spriteSheets.Add(Resources.Load<Texture>("Scavengers_SpriteSheet"));
        }
        GameManager.instance.terrainTypes.Add(new TerrainData(new Vector2(0,2/7), new Vector2(1/8,3/7), 2));
    }

    public bool isInBounds(Vector2 coords)
    {
        if (coords.x < 0 || coords.x >= columns || coords.y < 0 || coords.y >= rows)
            return false;
        else
            return true;
    }

}