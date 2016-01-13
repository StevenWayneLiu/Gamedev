using UnityEngine;
using System.Collections;

//template for character data class in game data
public class Character : IEntity {

    public enum Faction { Player, Enemy , NPC };//whether the character belongs to player or enemy
    public Faction fac;

    string name;
    public Attributes stats;//Entity Statistics
    public Inventory skills;//list of usable skills for this character
    public Inventory items;//list of items in this entity's inventory

    public CharacterManager manager;//used for accessing character in scene

	//default constructor
    public Character(CharacterManager man) : base()
    {
        fac = Faction.Enemy;
        manager = man;

        stats = new Attributes();
        skills = new Inventory();
        items = new Inventory();

        skills.AddItem(new Skill());//base skill
    }
    //copy constructor
    public Character(CharacterManager man, Character orig)
    {
        fac = orig.fac;
        manager = man;

        stats = new Attributes(orig.stats);//clone stats
        skills = new Inventory(orig.skills);//clone skills
        items = new Inventory(orig.items);//clone items

        skills.AddItem(new Skill());//base skill
    }

    public Character(Faction faction, CharacterManager man)
    {
        fac = faction;
        manager = man;

        stats = new Attributes();
        skills = new Inventory();
        items = new Inventory();
        
        skills.AddItem(new Skill());//base skill
    }

    public CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    //properties
    public string Name 
    {
        get { return name; }
        set { name = value; }
    }
    public float MaxHealth
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }
    public float CurHealth
    {
        get { return stats.curHealth; }
        set { stats.curHealth = value; }
    }
    //returns a float from 0 to 1 representing how much health is left
    public float RemHealth
    {
        get { return (float)(stats.curHealth / stats.maxHealth); }
    }
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
        if(CurHealth <=0)
        {
            manager.Death();
        }
    }

}
