using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    protected float xVel = 15f;
    protected float yVel = 0f;
    protected Vector2 travel;//keeps track of distance traveled from start position
    protected int damageVal = 10;//damage value for projectile
    protected Vector2 initPos;

    public float skillMultiplier = 1.3f;//damage multiplier that comes from skill
    public float charAttack = 1f;//attack value of character

    Attributes stats = new Attributes();

    protected float lifeTime = 2.0f;

    void Start()
    {
        stats.curHealth = -1*damageVal;
        stats.maxHealth = 0;
    }

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
            GetComponent<Rigidbody2D>().velocity = gameObject.transform.right*xVel;
    }

    protected void OnTriggerEnter2D(Collider2D collider)//do stuff when bullet hits target
    {
        Debug.Log("bullet hit");
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Entity")//if gameObject hit isn't a bullet and this bullet is active
        {
            if (entity.GetComponent<Character>() != null)
            {
                CalculateDamage(entity.GetComponent<Character>());//add bullet damage to stats
            }
            Deactivate();//deactivate bullet
        }
    }

    void CalculateDamage(IEntity targ)
    {
        float rawAtk = charAttack * skillMultiplier;
        targ.CurHealth -= rawAtk/(rawAtk + targ.Defense) * rawAtk;
    }
    
}
