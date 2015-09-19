using UnityEngine;
using System.Collections;

public class CharacterBaseClass {

    public enum Faction { Player, Enemy };//whether the character belongs to player or enemy
    public Faction fac;

    //Statistics
    private CharacterStats stats = new CharacterStats();
    private string name;
    private float timePoints = 100;
    private float bankedTP;

    public CharacterManager manager;//used for accessing character in scene

    //default constructor
    public CharacterBaseClass()
    {
        name = "base";
        timePoints = 100;
        bankedTP = 0;
    }
    //constructor for setting faction
    public CharacterBaseClass(Faction faction, CharacterManager man)
    {
        name = "base";
        fac = faction;
        stats.curHealth = stats.maxHealth;
        stats.strength = 100;
        stats.speed = 1;
        stats.move = 5;
        stats.range = 3;
        timePoints = 100;
        bankedTP = 0;
        manager = man;
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
    public float TimePoints 
    {
        get { return timePoints; }
        set { timePoints = value; }
    }
    public float BankedTP
    {
        get { return BankedTP; }
        set { bankedTP = value; }
    }
    public CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    //apply damage to target
    public void Attack(CharacterBaseClass target)
    {
        target.CurHealth -= stats.strength;
        timePoints -= 50;
    }

    

    
    
    
}
