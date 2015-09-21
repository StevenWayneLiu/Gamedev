using UnityEngine;
using System.Collections;

public class Skill : CharacterStats{

    public CharacterStats wepStats;//used when factoring character's equipment into damage

    //constructor
    public Skill()
    {
        curHealth = 0;
        maxHealth = 0;

        //calculate skill stats based on wepstats and character stats

    }

}
