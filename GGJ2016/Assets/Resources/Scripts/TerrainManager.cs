using UnityEngine;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainManager : MonoBehaviour
{
    public static TerrainManager instance;
	public int columns = 8; 								        //Number of columns in our game board.
	public int rows = 8;									        //Number of rows in our game board.
    public float tileSize = 1;                                      //number of world units per grid unit
    public int[,] grid;                                             //Holds all floor tiles
	public GameObject exit;									        //Prefab to spawn for exit.

    //mesh/collision information
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector3> colVertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    int textWidth;                                                  //the fraction of the sprite sheet's height for one tile
    int textHeight;                                                 //the fraction of the sprite sheet's width for one tile
    List<Vector2> toRender = new List<Vector2>();                   //list of items to render each pass
    public Dictionary<Vector2, List<Actor>> gridContents;                 //Holds contents of tiles

    void Awake()
    {
        if (instance == null)//Check if instance already exists
            instance = this;
        else if (instance != this)//if there is and it's not this, destroy this
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        LoadTerrainTypes();
        InitializeGridArray();
        GenerateTerrainData();
        GenMesh();
        
    }

	//Generate the array holding tile data
	void InitializeGridArray ()
	{
        //create grid array
        grid = new int[columns,rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                grid[x,y] = 0;
                toRender.Add(new Vector2(x, y));//set tile to be rendered
            }
        }
	}
		
    //Generates map features by setting terrain data		
	void GenerateTerrainData ()
	{

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
        for (int y = 0; y < rows; y++)
        {
            for(int x = 0; x < columns; x++)
            {
                uvs.Add(new Vector2(0, 2 / 7f));
                uvs.Add(new Vector2(0 / 8f, 3 / 7f));
                uvs.Add(new Vector2(1 / 8f, 3 / 7f));
                uvs.Add(new Vector2(1 / 8f, 2 / 7f));
            }
        }
        RenderMap();

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
                int place = ((int)toRender[i].y * columns + (int)toRender[i].x)*4;
                //find the terrain type at grid location
                int type = grid[(int)toRender[i].x, (int)toRender[i].y];
                //find the correct tile on sprite sheet for terrain type
                Vector2 uvBL = GameManager.instance.terrainTypes[type].normUV;
                Vector2 uvUR = GameManager.instance.terrainTypes[type].normUV2;
                Debug.Log(uvBL);
                Debug.Log(uvUR);
                uvs[place] = uvBL;
                uvs[place+1] = new Vector2(uvBL.x, uvUR.y);
                uvs[place+2] = uvUR;
                uvs[place+3] = new Vector2(uvUR.x, uvBL.y);
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
        //GameManager.instance.terrainTypes.Add(new TerrainData(new Vector2(0, 2 / 7f), new Vector2(1 / 8f, 3 / 7f), 2));
        GameManager.instance.terrainTypes.Add(new TerrainData(new Vector2(1 / 8f, 2 / 7f), new Vector2(2 / 8f, 3 / 7f), 2));
    }

    public void editTerrain(Vector2[] coords, int type)
    {
        foreach (Vector2 coord in coords)
        {
            grid[(int)coord.x, (int)coord.y] = type;
            toRender.Add(coord);
        }
        RenderMap();
    }

    public bool isInBounds(Vector2 coords)
    {
        if (coords.x < 0 || coords.x >= columns || coords.y < 0 || coords.y >= rows)
            return false;
        else
            return true;
    }

    //getters
    
    public Vector3 WorldPos(Vector2 gridPos)
    {
        Vector3 wPos = new Vector3(gridPos.x, 0, gridPos.y);
        return wPos * tileSize + new Vector3(tileSize / 2f, 0, tileSize / 2f);
    }
    //return information of thing on tile at gridPos
    public List<Actor> GetTileContents(Vector2 gridPos)
    {
        return gridContents[gridPos];
    }
    //return tile information for tile at gridPos
    public TerrainData GetTileInfo(Vector2 gridPos)
    {
        return GameManager.instance.terrainTypes[grid[(int)gridPos.x, (int)gridPos.y]];
    }
    //return true if tile is able to be moved into
    //returns false if tile is impassable or occupied
    public bool isValidMove(Vector2 dest)
    {
        if (isInBounds(dest))
            return true;
        else
            return false;
    }

}