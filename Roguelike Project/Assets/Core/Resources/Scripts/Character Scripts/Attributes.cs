using UnityEngine;
using System.Collections;


public class Attributes {

    public string name;
    public float maxHealth;
    public float curHealth;
    public float strength;
    public float defense;
    public int speed;
    public int move;//movement range of character
    public int range;//attack range of character
    public Vector3 pos;//position of character object

    //constructor
    public Attributes()
    {
        name = "base";
        maxHealth = 100;
        curHealth = maxHealth;
        strength = 20;
        speed = 1;
        move = 5;
        range = 3;
    }
    //copy constructor
    public Attributes(Attributes orig)
    {
        name = orig.name;
        maxHealth = orig.maxHealth;
        curHealth = orig.curHealth;
        strength = orig.strength;
        speed = orig.speed;
        move = orig.move;
        range = orig.range;
    }


    //add target stats to character stats
    public virtual void Add(Attributes modify){
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
