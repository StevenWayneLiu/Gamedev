using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    public float velocity = 10f;
    private Vector3 start;
    private float travel;
    private int damageVal = 10;//damage value for projectile
	// Use this for initialization
	void Start () {
        start = gameObject.transform.position;
	}

    public int getDamage()
    {
        return damageVal;
    }

	// Update is called once per frame
	void Update () {
        travel = gameObject.transform.position.x - start.x;
        if (travel > 10)
            GameObject.Destroy(gameObject);
	}
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity,0f);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Enemy")//if gameObject hit is a bullet
            GameObject.Destroy(gameObject);//destroy bullet if hit enemy
    }
    
}
