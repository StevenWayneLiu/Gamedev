using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    public float velocity = 10f;
    private float travel;
    private int damageVal = 10;//damage value for projectile
    private Vector3 buff;
    private bool isFired = false;
    private float start;
    private float maxDist = 10f;//max distance that a bullet can fly
	// Use this for initialization
	void Start () {
        buff = gameObject.transform.position;
	}

    public int getDamage()
    {
        return damageVal;
    }
    //change fired state of bullet and move it to a target location
    public void setFired(bool fired,Vector3 pos)
    {
        isFired = fired;//change the fired state
        gameObject.transform.position = pos;//move bullet to corresponding position
        start = pos.x;//set start position for bullets to x
    }

	// Update is called once per frame
	void Update () {
        travel = gameObject.transform.position.x - start;//keep track of distance traveled from start position
        if (Mathf.Abs(travel) > maxDist)//if bullet reaches the maximum distance
            setFired(false,buff);//deactivate bullet and move it to the buffer
	}
    void FixedUpdate()
    {
        if(isFired)
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity,0f);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
    void OnTriggerEnter2D(Collider2D collider)//do stuff when bullet hits target
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Entity" && isFired)//if gameObject hit isn't a bullet and the bullet is active
        {
            setFired(false,buff);//deactivate bullet and move to buffer
        }
    }
    
}
