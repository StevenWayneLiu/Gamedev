using UnityEngine;
using System.Collections;

public class EnemyMoveScript : MonoBehaviour {
    private float time = 0f;
    private float xVel = 0f;
    private float yVel = 5f;
    public float switchTime = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //AI movement
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
