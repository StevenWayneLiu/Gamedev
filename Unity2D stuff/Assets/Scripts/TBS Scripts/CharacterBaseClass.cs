using UnityEngine;
using System.Collections;

public class CharacterBaseClass {

    //member variables
    private int health;
    private int strength;
    private int speed;

    //constructor
    public CharacterBaseClass()
    {
        health = 100;
        strength = 10;
        speed = 5;
    }

    
    public void attack(CharacterBaseClass target)
    {
        target.health -= strength;
    }

}
