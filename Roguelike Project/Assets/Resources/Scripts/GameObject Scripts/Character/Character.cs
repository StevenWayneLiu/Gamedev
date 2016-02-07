using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//holds methods for controlling character, animations, etc. Has references to character data.
public class Character : MonoBehaviour , IAttributes, IInteractable {

    public CharacterData charData;//character info associated with this character

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
            GameManager.instance.Characters.Add(charData);//add character data to the gamedata
        }
        else if(gameObject.tag == "Enemy")
        {
            //create new enemy data for this object
            charData = new CharacterData(CharacterData.Faction.Enemy, this);
            GameManager.instance.Enemies.Add(charData);//add character data to the gamedata
        }
        else if (gameObject.tag == "NPC")
        {
            //create new enemy data for this object
            charData = new CharacterData(CharacterData.Faction.NPC, this);
            GameManager.instance.Characters.Add(charData);//add character data to the gamedata
            rbody = gameObject.GetComponent<Rigidbody2D>();
        }
        gameObject.AddComponent<HPBar>();
        anim = GetComponent<Animator>();
        velocity = Vector2.zero;
        direction = Vector2.zero;
        rbody = GetComponent<Rigidbody2D>();
        charData.skills.Add(new Skill());
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
        if(velocity != Vector2.zero)
            direction = velocity.normalized;
    }

    //what this character does when interacted with
    public void Interact(IInteractable other)
    {
        ToggleInventory();
    }

    //open character's inventory for player to see
    public void ToggleInventory()
    {
        if(invScr.gameObject.activeInHierarchy)
            invScr.gameObject.SetActive(false);
        else
            invScr.gameObject.SetActive(true);
        //populate item list UI
        if (invScr.isActiveAndEnabled)
        {
            invScr.Refresh(this, Items);
        }
    }

    public void UseItem(ItemData item)
    {
        if (item.Equippable)
        {
            //if equippable
        }
        else//else if consumable item
        {
            //either modify stats
            //or spawn an item
        }
    }

    public void UseSkill(Skill skill, float angle)
    {
        skill.user = this;
        if (skill.collider)//if skill spawns a prefab object/relies on a collider to be applied
        {
            SkillObject skillObj;
            if (skill.spawnObject)
            {
                //spawn slightly in front of character
                GameObject obj = (GameObject)Instantiate(skill.prefab, rbody.transform.position + new Vector3(direction.x, direction.y, 0f).normalized * 0.5f + skill.startOffset, Quaternion.identity);         
                skillObj = obj.GetComponent<SkillObject>();
                obj.transform.Rotate(new Vector3(0, 0, angle));
            }
            else
            {
                skillObj = skill.skillObj;
            }
            //give the skillobj any references it needs, like player stats for damage calc, and trip its activation flag
            skillObj.Skill = skill;
            ActivateHitbox();
        }
        else//else if skill applies effect directly to target's stats
        {
            //find target
            List<Character> targets = FindNear(rbody.transform);
            for (int i = 0; i < skill.MaxTargets; i++)
            {
                //apply skill to target
                skill.CalculateDamage(targets[i]);
            }
            
        }
    }

    //functions for finding targets from raycast/overlap
    public List<Character> FindNear(Transform origin)
    {
        List<Character> targs = new List<Character>();
        Collider2D[] cols;
        //circlecast targets
        cols = Physics2D.OverlapCircleAll(new Vector2(origin.position.x, origin.position.y), findRange, 1 << 9);
        foreach (var collider in cols)
        {
            if (collider.GetComponent<Character>())
                targs.Add(collider.gameObject.GetComponent<Character>());
        }
        return targs;
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
    //allow skill to apply to anything hit by a given collider for a certain duration
    public void ActivateHitbox()
    {
        //turn on flag in projectile's script
    }
    public void DeactivateHitbox()
    {
        //turn off flag in projectile's script
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
        get { return charData.Attack; }
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
