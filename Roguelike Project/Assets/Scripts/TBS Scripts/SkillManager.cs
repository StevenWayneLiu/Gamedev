using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

    public List<Skill> skills;//holds skills

    public void AddSkill(Skill s)
    {
        skills.Add(s);
    }

    public void RemoveSkill(int index)
    {
        skills.RemoveAt(index);
    }

    public Skill GetSkill(int index)
    {
        return skills[index];
    }

    /*
    public void ModifyGroup(ArrayList group, int stat, float amt)
    {
        for (int i = 0; i < group.Count; i++)
        {
            Modify((CharacterBaseClass)group[i], stat, amt);
        }
    }
    */

}
