using UnityEngine;
using System.Collections;
using System.Collections.Generic;//need for lists

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class RoomGen : MonoBehaviour {

    public byte[,] rooms;
    public byte[,] tiles;
    public int mapSize = 5;
    public int roomSize = 5;

    private Mesh mesh;
    private MeshCollider col;

    private List<Vector3> newVertices = new List<Vector3>();//holds the coordinates for each vertice
    private List<int> newTriangles = new List<int>();//holds the index of newVertices[] where the verts for each triangle are stored
    private List<Vector2> newUV = new List<Vector2>();//holds the cordinates on the texture sheet that define the UV's texture
    private int squareCount;//the number of the quad currently being generated

    private Vector2 grass = new Vector2(5,16);

    private float tileUnitX = 0.125f; //fraction of horizontal space one tile takes up on the texture
    private float tileUnitY = 0.0625f; //fraction of vertical space one tile takes up on the texture

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;//get the mesh filter
        col = GetComponent<MeshCollider>();//get the mesh collider

        rooms = new byte[mapSize, mapSize];
        tiles = new byte[roomSize, roomSize];

        GenMap();
        UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //randomly instantiates room content in the middle of a room
    void FillRoom(Vector3 pos, GameObject room)
    {
        int content = (int)(Random.value * 3);
        switch (content)
        {
            //add room content to the room script
            case 0:
                room.GetComponent<RoomScript>().content = (GameObject)Instantiate(Resources.Load("Battler"), pos, Quaternion.identity);
                break;
            case 1:
                room.GetComponent<RoomScript>().content = (GameObject)Instantiate(Resources.Load("Battler"), pos, Quaternion.identity);
                break;
            case 2:
                room.GetComponent<RoomScript>().content = (GameObject)Instantiate(Resources.Load("Stairs"), pos, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    void GenMap()
    {
        //fill up the map
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                //spawn map and object
                GenRoom(x * roomSize, z * roomSize);
            }
        }
    }

    //fill a room with tiles, given the coordinates of the room itself
    void GenRoom(int xPos, int zPos)
    {
        for (int x = 0; x < roomSize; x++)
        {
            for (int z = 1; z < roomSize + 1; z++)
            {
                GenSquare(xPos + x, zPos + z, grass);//start from room origin and fill up room
            }
        }
    }

    /*generates a quad at the given location of x and y, using the 
     * tile texture on the texture sheet at the coordinate of "textCoord"
    */
    void GenSquare(int x, int z, Vector2 textCoord)
    {
        
        newVertices.Add(new Vector3(x, 0, z));
        newVertices.Add(new Vector3(x + 1, 0, z));
        newVertices.Add(new Vector3(x + 1, 0, z - 1));
        newVertices.Add(new Vector3(x, 0, z - 1));

        newTriangles.Add(squareCount * 4);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 3);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 2);
        newTriangles.Add((squareCount * 4) + 3);

        //adding the texture to a face
        newUV.Add(new Vector2(tileUnitX * textCoord.x, tileUnitY * textCoord.y + tileUnitY));
        newUV.Add(new Vector2(tileUnitX * textCoord.x + tileUnitX, tileUnitY * textCoord.y + tileUnitY));
        newUV.Add(new Vector2(tileUnitX * textCoord.x + tileUnitX, tileUnitY * textCoord.y));
        newUV.Add(new Vector2(tileUnitX * textCoord.x, tileUnitY * textCoord.y));

        squareCount++;
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        newVertices.Clear();
        newTriangles.Clear();
        newUV.Clear();
        squareCount = 0;

        //create collision mesh
        col.sharedMesh = mesh;
    }


}
