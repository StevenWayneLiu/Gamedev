using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill : Entity{

    public CharacterStats wepStats;//used when factoring character's equipment into damage

    //default constructor
    public Skill() : base()
    {
        wepStats = new CharacterStats();
        CurHealth = -5;
        MaxHealth = 0;
        //calculate skill stats based on wepstats and character stats
    }

    //generic use function for all skills, may need to take in parameters
    //for the user character and ally characters at some point.
    public void Use()
    {
        List<CharacterManager> targets;//list for holding skill's current target
    }

}
