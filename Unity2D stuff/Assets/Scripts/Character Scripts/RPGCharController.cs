using UnityEngine;
using System.Collections;

public class RPGCharController : MonoBehaviour {

    public Entity charData;
    public Rigidbody2D rgBody;
    public float maxSpeed = 1f;//speed in units per second
    public float xDir;
    public float yDir;
    Vector2 velocity;

    // Use this for initialization
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        Move();
    }

    //move character based on directional key input
    void Move()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");

        velocity = new Vector2(xDir, yDir).normalized;//direction vector
        velocity *= maxSpeed * Time.deltaTime;//add frame-independent magnitude

        rgBody.MovePosition(rgBody.position + velocity);
    }

    void Attack()
    {

    }

}
