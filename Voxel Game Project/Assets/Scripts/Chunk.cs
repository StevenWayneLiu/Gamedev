using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour {

    Block[, ,] blocks;
    public static int chunkSize = 16;
    public bool update = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Block GetBlock(int x, int y, int z)
    {
        return blocks[x, y, z];
    }
    //updates chunk based on its contents
    void UpdateChunk()
    {

    }

    /*Sends the calculated mesh information to the mesh and collision components
    */
    void RenderMesh()
    {

    }
}
