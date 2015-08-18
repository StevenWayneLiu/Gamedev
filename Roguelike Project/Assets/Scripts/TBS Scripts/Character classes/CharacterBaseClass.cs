using UnityEngine;
using System.Collections;

public class CharacterBaseClass {

    public enum Faction { Player, Enemy };//whether the character belongs to player or enemy
    public Faction fac;

    //Statistics
    private string name;
    private float maxHealth = 100;
    private float curHealth;
    private int strength;
    private int speed;
    private int move;//movement range of character
    private int range;//attack range of character
    private float timePoints = 100;
    private float bankedTP;

    private GameObject battler;//battler character in the game
    private CharacterManager manager;//used for accessing character in scene

    //default constructor
    public CharacterBaseClass()
    {
        name = "base";
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
        move = 5;
        range = 3;
        timePoints = 100;
        bankedTP = 0;
    }
    //constructor for setting faction
    public CharacterBaseClass(Faction faction, CharacterManager man)
    {
        name = "base";
        fac = faction;
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
        move = 5;
        range = 3;
        timePoints = 100;
        bankedTP = 0;
        manager = man;
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
        get { return timePoints; }
        set { timePoints = value; }
    }
    public float BankedTP
    {
        get { return BankedTP; }
        set { bankedTP = value; }
    }
    public GameObject Battler
    {
        get { return battler; }
        set { battler = value; }
    }
    public CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    //apply damage to target
    public void Attack(CharacterBaseClass target)
    {
        target.CurHealth -= strength;
        timePoints -= 50;
    }

    

    
    
    
}
