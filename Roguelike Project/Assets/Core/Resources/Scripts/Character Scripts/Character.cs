using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//holds methods for controlling character, animations, etc. Has references to character data.
public class Character : MonoBehaviour , IEntity {

    public CharacterData charData;//character info associated with this character

    public SkillManager skillMan;
    public InventoryScreen invScr;//inventory screen
    Rigidbody2D rbody;
    float findRange = 1f;//radius of circlecast for finding npcs
    Vector2 velocity;//current character velocity
    Vector2 direction;//current direction character is facing
    public Animator anim;
    public float maxSpeed = 2f;//unity units per second
    

    public void Start()
    {
        if (gameObject.tag == "Player")//if this is a player object
        {
            //create character data for this object
            charData = new CharacterData(CharacterData.Faction.Player, this);
            GameData.data.Characters.Add(charData);//add character data to the gamedata
        }
        else if(gameObject.tag == "Enemy")
        {
            //create new enemy data for this object
            charData = new CharacterData(CharacterData.Faction.Enemy, this);
            GameData.data.Enemies.Add(charData);//add character data to the gamedata
        }
        else if (gameObject.tag == "NPC")
        {
            //create new enemy data for this object
            charData = new CharacterData(CharacterData.Faction.NPC, this);
            GameData.data.Characters.Add(charData);//add character data to the gamedata
            rbody = gameObject.GetComponent<Rigidbody2D>();
        }
        gameObject.AddComponent<HPBar>();
        anim = gameObject.GetComponent<Animator>();
        velocity = Vector2.zero;
        direction = Vector2.zero;
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

	
	// Update is called once per frame
	void Update () 
    {
        if(charData.stats.curHealth <= 0)
        {
            Death();
        }

	}
    void FixedUpdate()
    {
        //update animator
        UpdateAnimator();
    }


    //Animations
    public void UpdateAnimator()
    {
        if (velocity == Vector2.zero)
            anim.SetBool("isMoving", false);
        else
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("input_x", velocity.x);
            anim.SetFloat("input_y", velocity.y);
        }
    }
    //Sound

    //Controls
    public void Move(float xDir, float yDir)
    {
        rbody.velocity = Vector2.ClampMagnitude(new Vector2(xDir, yDir), 1f)*maxSpeed;
        velocity = rbody.velocity;
        direction = velocity;
    }

    //what this character does when interacted with
    public void Interact()
    {
        ToggleInventory();
    }

    public Character FindNearest()
    {
        Character targ = null;
        Collider2D[] cols;
        //circlecast targets
        cols = Physics2D.OverlapCircleAll(new Vector2(rbody.transform.position.x, rbody.transform.position.y), findRange, 1 << 9);
        //loop through and find closest target
        Transform nearest = null;
        foreach (var i in cols)
        {
            if (i.gameObject.GetComponent<Character>() != null && i.tag == "NPC")
            {
                if (nearest == null)
                    nearest = i.transform;
                else if (Vector3.Distance(rbody.transform.position, nearest.position) > Vector3.Distance(rbody.transform.position, i.transform.position))
                {
                    nearest = i.transform;
                }
            }
        }

        if (nearest == null)
            Debug.Log("No targets found");
        else
            targ = nearest.gameObject.GetComponent<Character>();
        return targ;
    }

    void ToggleInventory()
    {
        if(invScr.gameObject.activeInHierarchy)
            invScr.gameObject.SetActive(false);
        else
            invScr.gameObject.SetActive(true);
        //spawn in objects
        //populate item list UI
        if (invScr.isActiveAndEnabled)
        {
            for (int i = 0; i < charData.items.Count; i++)
            {
                invScr.AddButton(charData.items[i].Name);

            }
        }
    }




    //properties

    public string Name
    {
        get { return charData.Name; }
        set { charData.Name = value; }
    }
    public float MaxHealth
    {
        get { return charData.MaxHealth; }
        set { charData.MaxHealth = value; }
    }
    public float CurHealth
    {
        get { return charData.CurHealth; }
        set { charData.CurHealth = value; }
    }
    public float RemHealth
    {
        get { return charData.RemHealth; }
    }
    public float Attack
    {
        get { return charData.Defense; }
        set { charData.Attack = value; }
    }
    public float Defense
    {
        get { return charData.Defense; }
        set { charData.Defense = value; }
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    public List<ItemData> Items
    {
        get { return charData.items; }
        set { charData.items = value; }
    }
}
