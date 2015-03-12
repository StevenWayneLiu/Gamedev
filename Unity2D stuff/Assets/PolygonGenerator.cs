﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour {
	
	// This first list contains every vertex of the mesh that we are going to render
	public List<Vector3> newVertices = new List<Vector3>();
	public List<int> newTriangles = new List<int>();
	public List<Vector2> newUV = new List<Vector2>();
	
	public List<Vector3> colVertices = new List<Vector3>();
	public List<int> colTriangles = new List<int>();
	private int colCount;
	
	private Mesh mesh;
	private MeshCollider col;
	
	private float tUnit = 0.25f; //amount of space one tile takes up on the texture
	private Vector2 tStone = new Vector2(1,0);
	private Vector2 tGrass = new Vector2(0,1);
	
	public byte[,] blocks;
	private int squareCount;
	
	public bool update = false;
	
	
	// Use this for initialization
	void GenTerrain(){
		blocks = new byte[96,128];
		
		for(int px = 0; px < blocks.GetLength(0); px++){
			int stone = Noise(px,0,80,15,1);
			stone += Noise(px,0,50,30,1);
			stone += Noise(px,0,10,10,1);
			stone+=75;
			
			int dirt = Noise(px,0,100,35,1);
			dirt += Noise(px,0,50,30,1);
			dirt += 75;
			
			for(int py = 0; py < blocks.GetLength(1); py++){
				if(py<stone){
					blocks[px,py]=1;
					
					if(Noise(px,py,12,16,1)>10){
						blocks[px,py] = 2;
					}
					
					if(Noise(px,py*2,16,14,1)>10){
						blocks[px,py]=0;
					}
				}
				else if(py<dirt) {
					blocks[px,py]=2;
				}
			}
		}
	}
	void BuildMesh(){
	
		for(int px=0; px < blocks.GetLength(0); px++){
			for(int py = 0; py < blocks.GetLength(1); py++){
				if(blocks[px,py]!=0){
				GenCollider(px,py);
					if(blocks[px,py] == 1){
						GenSquare(px,py,tStone);
					}
					else if(blocks[px,py] == 2){
						GenSquare(px,py,tGrass);
					}
				}
			}
		}
		
	}
	
	byte Block (int x, int y){
		if(x==-1||x==blocks.GetLength(0)||y==-1||y==blocks.GetLength(1)){
			return (byte)1;
		}
		return blocks[x,y];
	}

	void GenCollider(int x, int y){
		//top
		if(Block(x,y+1)==0){
			colVertices.Add( new Vector3(x,y,1));
			colVertices.Add( new Vector3(x+1,y,1));
			colVertices.Add( new Vector3(x+1,y,0));
			colVertices.Add( new Vector3(x,y,0));
			
			ColliderTriangles();
			colCount++;
		}
		//bot
		if(Block(x,y-1)==0){
			colVertices.Add( new Vector3(x,y-1,0));
			colVertices.Add( new Vector3(x+1,y-1,0));
			colVertices.Add( new Vector3(x+1,y-1,1));
			colVertices.Add( new Vector3(x,y-1,1));
			
			ColliderTriangles();
			colCount++;
		}
		//left
		if(Block(x-1,y)==0){
			colVertices.Add( new Vector3(x,y-1,1));
			colVertices.Add( new Vector3(x,y,1));
			colVertices.Add( new Vector3(x,y,0));
			colVertices.Add( new Vector3(x,y-1,0));
			
			ColliderTriangles();
			colCount++;
		}
		//right
		if(Block(x+1,y)==0){
			colVertices.Add( new Vector3(x+1,y,1));
			colVertices.Add( new Vector3(x+1,y-1,1));
			colVertices.Add( new Vector3(x+1,y-1,0));
			colVertices.Add( new Vector3(x+1,y,0));
			
			ColliderTriangles();
			colCount++;
		}
	}

	void ColliderTriangles(){
		colTriangles.Add(colCount*4);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+3);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+2);
		colTriangles.Add((colCount*4)+3);
	}
	
	void GenSquare(int x,int y, Vector2 texture){
	
	newVertices.Add( new Vector3 (x  , y  , 0 ));
	newVertices.Add( new Vector3 (x + 1 , y  , 0 ));
	newVertices.Add( new Vector3 (x + 1 , y-1  , 0 ));
	newVertices.Add( new Vector3 (x  , y-1  , 0 ));
		   
	newTriangles.Add(squareCount*4);
	newTriangles.Add((squareCount*4)+1);
	newTriangles.Add((squareCount*4)+3);
	newTriangles.Add((squareCount*4)+1);
	newTriangles.Add((squareCount*4)+2);
	newTriangles.Add((squareCount*4)+3);
	
	newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
	newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
	newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
	newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));
	
	squareCount++;
	
	}
	
	// Update is called once per frame
	void UpdateMesh() {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
		
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();
		squareCount = 0;
		
		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();
		col.sharedMesh = newMesh;
		
		colVertices.Clear();
		colTriangles.Clear();
		colCount=0;
	}
	

	int Noise(int x, int y, float scale, float mag, float exp){
	return (int)(Mathf.Pow ((Mathf.PerlinNoise(x/scale,y/scale)*mag),(exp)));
	}
	void Start () {
		 
		mesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();
		   
		//float x = transform.position.x;
		//float y = transform.position.y;
		//float z = transform.position.z;
		

		
		GenTerrain();
		BuildMesh();
		UpdateMesh();
		
	}
	void Update() {
		if(update){
			BuildMesh();
			UpdateMesh();
			update=false;
		}
	}

}
