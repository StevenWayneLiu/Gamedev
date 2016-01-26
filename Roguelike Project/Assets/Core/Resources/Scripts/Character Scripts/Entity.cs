using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Generic entity class, container for functionality

public class Entity : IEntity {

    //member variables
    string name;
    public Attributes stats;//Entity Statistics
    public Inventory<Skill> skills;//list of usable skills for this character
    public Inventory<ItemData> items;//list of items in this entity's inventory
    

    //default constructor
    public Entity()
    {
        stats = new Attributes();
        skills = new Inventory<Skill>();
        items = new Inventory<ItemData>();
    }
    //copy constructor
    public Entity(Entity orig)
    {
        stats = new Attributes(orig.stats);//clone stats
        skills = new Inventory<Skill>(orig.skills);//clone skills
        items = new Inventory<ItemData>(orig.items);//clone items
    }

    //properties
    public string Name { get; set; }
    public float MaxHealth { get; set; }
    public float CurHealth
    {
        get { return stats.curHealth; }
        set { stats.curHealth = value; }
    }
    public float RemHealth//returns a float from 0 to 1 representing how much health is left
    {
        get
        {
            return (float) (stats.curHealth / stats.maxHealth);
        }
    }
    //attack potency value
    
    public float Attack
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float Defense
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public void Death()
    {

    }
}
