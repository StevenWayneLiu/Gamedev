using UnityEngine;
using System.Collections;

public class CharacterBaseClass {

    public enum Faction { Player, Enemy };//whether the character belongs to player or enemy

    public Faction fac;

    //member variables
    private string name;
    private float maxHealth = 100;
    private float curHealth;
    private int strength;
    private int speed;
    private int move;//movement range of character
    private int range;//attack range of character
    private float timePoints;
    private float bankedTP;
    

    //default constructor
    public CharacterBaseClass()
    {
        name = "base";
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
        move = 5;
        range = 3;
    }
    //constructor for setting faction
    public CharacterBaseClass(Faction faction)
    {
        name = "base";
        fac = faction;
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
    }

    //properties
    public float MaxHealth { get; set; }
    public float CurHealth
    {
        get { return curHealth; }
        set { curHealth = value; }
    }
    public float HealthFract//returns a float from 0 to 1 representing how much health is left
    {
        get
        {
            return (float) (curHealth / maxHealth);
        }
    }
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Move { get; set; }
    public int Range { get; set; }
    public float TimePoints 
    {
        get { return TimePoints; }
        set { TimePoints = value; }
    }
    public float BankedTP
    {
        get { return BankedTP; }
        set { BankedTP = value; }
    }
    
    //apply damage to target
    public void Attack(CharacterBaseClass target)
    {
        target.CurHealth -= strength;
        timePoints -= 50;
    }

    

    
    
    
}
