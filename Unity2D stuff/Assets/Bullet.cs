using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float velocity = 10f;
    private Vector3 start;
    private float travel;
	// Use this for initialization
	void Start () {
        start = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        travel = gameObject.transform.position.x - start.x;
        if (travel > 10)
            DestroyObject(gameObject);
	}
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity,0f);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        DestroyObject(gameObject);
    }
}
