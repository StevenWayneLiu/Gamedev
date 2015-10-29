using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//skill manager as an alernative to Entity-skills, do not use concurrently with Entity-skills
[RequireComponent (typeof (CharacterManager))]
public class CharSkills : MonoBehaviour{

    public List<CharacterStats> skills;//list of existing skills

	// Use this for initialization
	void Start () {
        //add new starting skills to manager from the master skill list
        skills.Add(new CharacterStats());
	}
	
	
    //use an existing skill in the skill list
    public void Use(int index, CharacterStats targ)
    {
        Use(skills[index], targ);
    }

    //takes in a struct that specifies skill type, executes skill
    //for now only takes characterstats
    public void Use(CharacterStats stats, CharacterStats targ)
    {
        targ.Add(stats);
    }

    

}
