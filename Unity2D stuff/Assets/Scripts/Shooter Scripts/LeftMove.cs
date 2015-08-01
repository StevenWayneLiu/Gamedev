using UnityEngine;
using System.Collections;

public class LeftMove : EnemyMoveScript {
    public float switchTime = 0.1f;
	// Use this for initialization
	void Start () {
        xVel = -2f;
        yVel = 3f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > switchTime)
        {
            yVel *= -1f;
            time = 0f;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }
}
