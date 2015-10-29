using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharacterManager))]
public class CharSkills : SkillManager {

    public List<CharacterManager> targets;//list for holding character's current targets
    public List<GameObject> objs;

	// Use this for initialization
	void Start () {
        //add new starting skills to manager from the master skill list
        skills.Add(new Skill());//placeholder
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Physics.SphereCast(gameObject.transform.position,1f,gameObject.transform.forward, out hit, 3f);
            if (hit.collider && hit.collider.gameObject.tag == "Enemy")//only apply damage if cast hit something
            {
                Debug.Log("Character hit");
                objs.Add(hit.collider.gameObject);
                GetTargets(objs);
                UseSkill(new Skill());
            }
            
        }
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
        objs.Clear();
    }

}
