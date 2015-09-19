using UnityEngine;
using System.Collections;

public class Skill {

    public CharacterStats skillStats;//used when factoring character's stats into damage
    public CharacterStats wepStats;//used when factoring character's equipment into damage

    //constructor
    public Skill()
    {
        skillStats = new CharacterStats();
        skillStats.curHealth = 0;

    }
}
