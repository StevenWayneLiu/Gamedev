using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour {

    public enum Stats { curHealth, maxHealth, strength, speed };

    private Stats targStat;


    public void ModifyGroup(ArrayList group, int stat, float amt)
    {
        for (int i = 0; i < group.Count; i++)
        {
            Modify((CharacterBaseClass)group[i], stat, amt);
        }
    }

    //modify stat
	public void Modify(CharacterBaseClass target, int stat, float amt)
    {
        targStat = (Stats) stat;
        switch(targStat)
        {
            case(Stats.curHealth):
                target.CurHealth += amt;
                break;
            case(Stats.maxHealth):
                target.MaxHealth += amt;
                break;
            default:
                break;
        }
    }
}
