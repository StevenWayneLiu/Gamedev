using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill : Entity{

    public List<Character> targets;//list for holding skill's current target
    protected int targetLim = 0;//max number of targets

    public bool collider = true;//whether skill uses a collider for finding targets
    public bool cast = false;//if the skill uses a raycast/shapecast for finding targets
    public float duration = 1;//duration that skill checks for detection, if not determined by animation


    //default constructor
    public Skill() : base()
    {
        CurHealth = -5;
        MaxHealth = 0;
        //calculate skill stats based on wepstats and character stats
    }

    //generic use function for all skills, may need to take in parameters
    //for the user character and ally characters at some point.
    public virtual void Use()
    {
        
    }
    //a deactivate skill might be useful for skills that have a duration effect

}
