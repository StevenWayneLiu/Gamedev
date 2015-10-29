using UnityEngine;
using System.Collections;

public class FlyerController2D : MonoBehaviour {

    //movement variables
    public float maxSpeed = 5f;
    Vector2 vel;//velocity vector of character
    Vector2 dir;//direction character is "facing", and thus where their attacks land
    private bool fire = false;

    //bullet buffer stuff
    public GameObject bullet;
    private BulletPool bullets;//bullet buffer array, holds bullets inside

    // Use this for initialization
    void Start()
    {
        bullets = gameObject.GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input from controls
        fire = Input.GetButton("Fire1");

        if (fire)
        {
            SkillList.skillList.allSkills[0].Use(this.transform);
        }
    }
    void FixedUpdate()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //ensure magnitude of total velocity vector doesn't exceed max speed
        vel = dir.normalized;
        GetComponent<Rigidbody2D>().velocity = vel*maxSpeed;
    }
}
