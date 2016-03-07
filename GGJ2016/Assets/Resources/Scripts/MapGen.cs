using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MapGen : MonoBehaviour 
{
    [SerializeField]int gridWidth = 100;
    [SerializeField]int gridLength = 100;
    [SerializeField]int minRoomSize = 10;
    [SerializeField]int tileSize = 1;
    int gridHeight = 3;//currently unused

    //mesh generation
    Mesh mesh;
    Tile[][] tiles;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector3> colVertices = new List<Vector3>();

    //UV's
    int textWidth;//the fraction of the sprite sheet's height for one tile
    int textHeight;//the fraction of the sprite sheet's width for one tile

    List<Vector3> toUpdate;//list of tiles to update each loop

	// Use this for initialization
	void Awake() 
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenMesh();
	}

    // Update is called once per frame
    void Update()
    {
	    //only look at and update the tiles in updateMesh
	}
    //store data for each tile in an array
    void GenMapData()
    {

    }

    //generate mesh for array
    void GenMesh()
    {
        //assign vertices
        for (int z = 0; z <= gridLength; z++)
        {
            for (int x = 0; x <= gridWidth; x++)
            {
                vertices.Add(new Vector3(x * tileSize, 0 * tileSize, z * tileSize));
            }
        }
        mesh.vertices = vertices.ToArray();

        //assign triangles
        for (int z = 0; z < gridLength * (gridWidth + 1); z += gridWidth + 1)//iterate rows
        {
            for (int x = 0; x < gridWidth; x++)//iterate columns
            {
                //lower triangle: 0, 3, 1
                triangles.Add(x + z);
                triangles.Add(x + z + gridWidth + 1);
                triangles.Add(x + z + 1);
                //upper triangle: 1, 3, 2
                triangles.Add(x + z + 1);
                triangles.Add(x + z + gridWidth + 1);
                triangles.Add(x + z + gridWidth + 2);
            }
        }
            
        mesh.triangles = triangles.ToArray();

        //assign normals
        Vector3[] normals = new Vector3[vertices.Count];
        for(int i = 0; i < vertices.Count; i++)
        {
            normals[i] = -Vector3.forward;
        }
        mesh.normals = normals;

        //assign UVs
        Vector2[] uvs = new Vector2[vertices.Count];
        for (int z = 0; z < gridLength * (gridWidth + 1); z += gridWidth + 1)//iterate rows
        {
            for (int x = 0; x < gridWidth; x++)//iterate columns
            {
                //lower triangle: 0, 3, 1
                uvs[x + z] = new Vector2(0, 0);
                uvs[x + z + gridWidth + 1] = new Vector2(0, 1);
                uvs[x + z + 1] = new Vector2(1, 0);
                uvs[x + z + gridWidth + 2] = new Vector2(1, 1);
            }
        }
        mesh.uv = uvs;

    }
    void GenCollisionMesh()
    {

    }
    void RenderMap()
    {

    }
}
