using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Skill : IAttributes{

    Attributes stats = new Attributes();
    public List<Character> targets;//list for holding skill's current target
    public Character user;
    protected int targetLim = 1;//max number of targets

    public Vector3 startOffset = new Vector3(0f, 0f, 0f);//an offset from the starting point to spawn projectiles at
    public float range;//casts: range of casting area; projectiles: distance projectile travels
    public bool collider = true;//whether skill uses a collider for finding targets
    public float duration = 1;//duration that skill checks for detection, if not determined by animation
    public GameObject prefab;
    public bool spawnObject = false;
    public bool ignoreDefense = false;//use for healing skills
    public float charAttack = 0f;
    public float skillMultiplier = 1f;
    public float velocity = 15;//movement speed of skill objects
    public SkillObject skillObj;


    //default constructor
    public Skill() : base()
    {
        //calculate skill stats based on wepstats and character stats
    }
    //apply modifiers to target stats
    public void CalculateDamage(IAttributes targ)
    {
        float baseAtk = skillMultiplier * user.Attack;
        targ.CurHealth -= Mathf.Clamp(baseAtk / (baseAtk + targ.Defense) * baseAtk, 0, targ.CurHealth);
    }

    public int MaxTargets
    {
        get { return targetLim; }
    }


    public string Name
    {
        get { return stats.name; }
        set { stats.name = value; }
    }

    public float MaxHealth
    {
        get { return stats.maxHealth; }
        set { stats.maxHealth = value; }
    }

    public float CurHealth
    {
        get { return stats.curHealth; }
        set { stats.curHealth = value; }
    }

    public float RemHealth
    {
        get { return stats.curHealth/stats.maxHealth; }
    }

    public float Attack
    {
        get { return stats.strength; }
        set { stats.strength = value; }
    }

    public float Defense
    {
        get { return stats.defense; }
        set { stats.defense = value; }
    }
}
