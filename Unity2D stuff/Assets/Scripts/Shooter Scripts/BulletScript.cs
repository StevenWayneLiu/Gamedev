using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    protected float xVel = 15f;
    protected float yVel = 0f;
    protected Vector2 travel;//keeps track of distance traveled from start position
    protected int damageVal = 10;//damage value for projectile
    protected Vector2 initPos;

    protected float lifeTime = 2.0f;

    protected void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
        initPos = gameObject.transform.position;
    }
    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }
    protected void OnDisable()
    {
        CancelInvoke();
    }

    public int getDamage()
    {
        return damageVal;
    }

	// Update is called once per frame
	protected void Update () {
        float xDist = gameObject.transform.position.x - initPos.x;
        float yDist = gameObject.transform.position.y - initPos.y;
        travel = new Vector2(xDist, yDist);//keep track of distance traveled from start position
	}

    void FixedUpdate()
    {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }

    protected void OnTriggerEnter2D(Collider2D collider)//do stuff when bullet hits target
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Entity")//if gameObject hit isn't a bullet and the bullet is active
        {
            Deactivate();//deactivate bullet
        }
    }
    
}
