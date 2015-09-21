using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharacterManager))]
public class CharSkills : SkillManager {

    public List<CharacterManager> targets;//list for holding character's current targets

	// Use this for initialization
	void Start () {
        //add new starting skills to manager from the master skill list
        AddSkill(new Skill());//placeholder
	}
	
	// Update is called once per frame
	void Update () {

	}

    //takes in a list of gameobjects and adds the associated character managers to targets
    public void GetTargets(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            CharacterManager man = objects[i].GetComponent<CharacterManager>();
            if (man && !targets.Contains(man))//check if object has a character manager and if targets already has it
            {
                targets.Add(objects[i].GetComponent<CharacterManager>());
            }
        }
    }

    //applies skill onto all entries in the target list
    public void UseSkill(Skill sk)
    {
        //get targets for the skill
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].charInfo.stats.Add(sk);//apply skill effects to target
        }
        //clear target list
        targets.Clear();
    }

}
