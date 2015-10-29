using UnityEngine;
using System.Collections;

public class CharacterStats {

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
        maxHealth = 100;
        curHealth = maxHealth;
        strength = 100;
        speed = 1;
        move = 5;
        range = 3;
    }

    //add target stats to character stats
    public virtual void Add(CharacterStats targ){
        maxHealth += targ.maxHealth;
        curHealth += targ.curHealth;
        strength += targ.strength;
        speed += targ.speed;
        move += targ.move;
        range += targ.range;
    }
}
