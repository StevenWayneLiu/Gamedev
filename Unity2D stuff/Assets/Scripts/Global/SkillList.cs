using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillList : MonoBehaviour{

    public static SkillList skillList;

    public List<Skill> allSkills = new List<Skill>();

    public void Awake()
    {
        skillList = this;
        allSkills.Add(new FireBullet());
    }

}
