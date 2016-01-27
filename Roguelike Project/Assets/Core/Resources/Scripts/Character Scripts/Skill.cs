using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill : Entity{

    public List<Character> targets;//list for holding skill's current target
    protected int targetLim = 0;//max number of targets

    public bool collider = true;//whether skill uses a collider for finding targets
    public bool cast = false;//if the skill uses a raycast/shapecast for finding targets
    public float duration = 1;//duration that skill checks for detection, if not determined by animation
    public GameObject prefab;
    public bool spawnObject = false;
    public float charAttack = 0f;
    public float skillMultiplier = 1f;

    //default constructor
    public Skill() : base()
    {
        CurHealth = -5;
        MaxHealth = 0;
        //calculate skill stats based on wepstats and character stats
    }

    public virtual void Use(IEntity targ)
    {
        //find target
        //apply damage
        CalculateDamage(targ);
    }
    //a deactivate skill might be useful for skills that have a duration effect

    //apply modifiers to target stats
    void CalculateDamage(IEntity targ)
    {
        float rawAtk = charAttack * skillMultiplier;
        targ.CurHealth -= rawAtk / (rawAtk + targ.Defense) * rawAtk;
    }

}
