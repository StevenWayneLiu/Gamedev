using UnityEngine;
using System.Collections;

public class RpgController : MonoBehaviour {

    //movement variables
    public float maxSpeed = 0.01f;//unity units per second
    private Vector2 vel;
    private bool fire = false;
    private float angle;
    public Camera cam;
    Rigidbody2D rbody;

    public Animator anim;

    //bullet buffer stuff
    public GameObject bullet;
    private BulletPool bullets;//bullet buffer array, holds bullets inside

    // Use this for initialization
    void Start()
    {
        vel = new Vector2(0, 0);
        angle = 0f;
        bullets = gameObject.GetComponent<BulletPool>();
        anim = gameObject.GetComponent<Animator>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input from controls
        fire = Input.GetButton("Fire1");
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        

        //find out angle of movement vector for figuring out magnitude of movement vector
        if (vel.x == 0)//if not moving on the x direction
            angle = (Mathf.PI / 2);//set the angle to pi/2
        else
            angle = Mathf.Atan(Mathf.Abs(vel.y / vel.x));//else, calculate angle for movement

        if (fire)//if firing and the cooldown is over, then fire
        {
            bullets.Fire(MouseDir());
        }
    }
    void FixedUpdate()
    {
        //update animator
        if (vel == Vector2.zero)
            anim.SetBool("isMoving", false);
        else
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("input_x", vel.x);
            anim.SetFloat("input_y", vel.y);
        }

        //ensure magnitude of total velocity vector doesn't exceed max speed
        rbody.velocity = Vector2.ClampMagnitude(vel * maxSpeed, maxSpeed);
    }


    float MouseDir()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 curPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 moveVec = mousePos - curPos;
        angle = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
        return angle;
    }
}
