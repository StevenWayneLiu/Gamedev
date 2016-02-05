using UnityEngine;
using System.Collections;

public class SkillObject : MonoBehaviour {
    protected Vector2 travel;//keeps track of distance traveled from start position
    protected Vector2 initPos;

    public Skill skill;

    protected float lifeTime = 2.0f;

    void Start()
    {
        
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

    public Skill Skill
    {
        get { return skill; }
        set { skill = value; }
    }

    void FixedUpdate()
    {
        float xDist = gameObject.transform.position.x - initPos.x;
        float yDist = gameObject.transform.position.y - initPos.y;
        travel = new Vector2(xDist, yDist);//keep track of distance traveled from start position
        GetComponent<Rigidbody2D>().velocity = gameObject.transform.right*skill.velocity;
    }

    protected void OnTriggerEnter2D(Collider2D collider)//do stuff when bullet hits target
    {
        Debug.Log("bullet hit");
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag != "Player")
        {
            Debug.Log("not player");
            if (entity.GetComponent<Character>() != null && entity.tag == "Enemy" && skill != null)
            {
                //calculate skill damage and apply to target
                skill.CalculateDamage(entity.GetComponent<Character>());
            }
            Deactivate();//deactivate bullet if it hits anything at all
        }
    }
    
}
