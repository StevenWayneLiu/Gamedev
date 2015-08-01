using UnityEngine;
using System.Collections;

public class CharacterBaseClass {

    public enum Faction { Player, Enemy };//whether the character belongs to player or enemy

    public Faction fac;

    //member variables
    private float maxHealth = 100;
    private float curHealth;
    private int strength;
    private int speed;
    private int move;//movement range of character
    private int range;//attack range of character

    //default constructor
    public CharacterBaseClass()
    {
        curHealth = maxHealth;
        strength = 10;
        speed = 5;
        move = 5;
        range = 3;
    }
    //constructor for setting faction
    public CharacterBaseClass(Faction faction)
    {
        fac = faction;
        curHealth = maxHealth;
        strength = 10;
        speed = 5;
    }

    //properties
    public float MaxHealth { get; set; }
    public float CurHealth { get; set; }
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

    //generic action script for buttons to access
    public void Act(int actIndex, CharacterBaseClass target)
    {
        switch (actIndex)
        {
            case (0):
                Wait();
                break;
            case(1):
                Attack(target);
                break;
            default:
                break;
        }
    }
    
    public void Attack(CharacterBaseClass target)
    {
        target.curHealth -= strength;
        TBBattleSystem.battleManager.EndTurn();
    }

    public void Modify(CharacterBaseClass target)
    {


    }

    public void Wait()
    {
        TBBattleSystem.battleManager.EndTurn();
    }

}
