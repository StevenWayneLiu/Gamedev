using UnityEngine;
using System.Collections;

public class CharacterStats {

    public string name;
    public float maxHealth = 100;
    public float curHealth;
    public int strength;
    public int speed;
    public int move;//movement range of character
    public int range;//attack range of character
    public Vector3 pos;//position of character object

    //constructor
    public CharacterStats()
    {
        name = "base";
        maxHealth = 100;
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
        move = 5;
        range = 3;
    }

    //add target stats to character stats
    public virtual void Add(CharacterStats modify){
        maxHealth += modify.maxHealth;
        curHealth += modify.curHealth;
        strength += modify.strength;
        speed += modify.speed;
        move += modify.move;
        range += modify.range;
    }

    public virtual void Zero()
    {
        maxHealth = 0;
        curHealth = 0;
        strength = 0;
        speed = 0;
        move = 0;
        range = 0;
    }
}
