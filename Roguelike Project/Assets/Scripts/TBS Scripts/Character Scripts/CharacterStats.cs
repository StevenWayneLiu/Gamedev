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

    //apply effects to target set of character stats
    public virtual void Add(CharacterStats targ){
        
    }
}
