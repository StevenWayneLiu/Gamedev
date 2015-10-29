using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Generic entity class, container for functionality
public class Entity {

    //member variables
    public CharacterStats stats;//Character Statistics
    public List<Skill> skills;//list of usable skills for this character
    

    //default constructor
    public Entity()
    {
        stats = new CharacterStats();
    }

    //properties
    public float MaxHealth { get; set; }
    public float CurHealth
    {
        get { return stats.curHealth; }
        set { stats.curHealth = value; }
    }
    public float HealthFract//returns a float from 0 to 1 representing how much health is left
    {
        get
        {
            return (float) (stats.curHealth / stats.maxHealth);
        }
    }
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Move { get; set; }
    public int Range { get; set; }
    

    //apply damage to target
    public void Attack(Entity target)
    {
        target.CurHealth -= stats.strength;
    }

    

    
    
    
}
