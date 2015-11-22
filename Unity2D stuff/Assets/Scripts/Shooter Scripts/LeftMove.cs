using UnityEngine;
using System.Collections;

public class LeftMove : MonoBehaviour {
    public float switchTime = 0.1f;
    public Vector2 vel;
    float time = 0;
	// Use this for initialization
	void Start () {
        vel = new Vector2(-2f,3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > switchTime)
        {
            vel.Scale(new Vector2(1f,-1f));
            time = 0f;
        }
        GetComponent<Rigidbody2D>().velocity = vel;
    }
}
