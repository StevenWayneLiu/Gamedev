using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//template for character data class in game data
[System.Serializable]
public class CharacterData : IEntity
{

    public enum Faction { Player, Enemy, NPC };//whether the character belongs to player or enemy
    public Faction fac;

    public Attributes stats;//Entity Statistics
    public Database<Skill> skills;//list of usable skills for this character
    public List<ItemData> items;//list of items in this entity's inventory
    

    public Character manager;//used for accessing character in scene

    //default constructor
    public CharacterData(Character man)
        : base()
    {
        fac = Faction.Enemy;
        manager = man;

        stats = new Attributes();
        skills = new Database<Skill>();
        items = new List<ItemData>();

        skills.AddEntry(new Skill());//base skill
    }
    //copy constructor
    public CharacterData(Character man, CharacterData orig)
    {
        fac = orig.fac;
        manager = man;

        stats = new Attributes(orig.stats);//clone stats
        skills = new Database<Skill>(orig.skills);//clone skills
        items = new List<ItemData>(orig.items);//clone items

        skills.AddEntry(new Skill());//base skill
    }

    public CharacterData(Faction faction, Character man)
    {
        fac = faction;
        manager = man;

        stats = new Attributes();
        skills = new Database<Skill>();
        items = new List<ItemData>();

        skills.AddEntry(new Skill());//base skill
    }

    public Character Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    //properties
    public string Name
    {
        get { return stats.name; }
        set { stats.name = value; }
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
        if (CurHealth <= 0)
        {
            manager.Death();
        }
    }

}
