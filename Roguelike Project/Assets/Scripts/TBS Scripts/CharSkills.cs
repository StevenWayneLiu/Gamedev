using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharacterManager))]
public class CharSkills : SkillManager {

    List<CharacterManager> targets;//list for holding character's current targets

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetTargets()
    {

    }

    public void UseSkill()
    {
        //get targets for the skill
        //apply skill parameters to target
    }
}
