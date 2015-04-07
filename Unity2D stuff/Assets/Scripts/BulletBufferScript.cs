using UnityEngine;
using System.Collections;

public class BulletBufferScript : MonoBehaviour {
    public int bulletCount = 10;
    public Transform bullt;//bullet prefab to be fired
    GameObject[] bulletBuffer;//array for holding enemy bullets
	// Use this for initialization
	void Start () {
	    //create enemies
        bulletBuffer = new GameObject[bulletCount];
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
