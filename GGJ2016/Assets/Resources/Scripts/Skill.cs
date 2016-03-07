using UnityEngine;
using System.Collections;

public class Skill {

    public string name;
    public Vector2[] area;      //attack area when character is facing up
    public float multiplier;    //effect multiplier

    public Skill()
    {
        area = new Vector2[1];
        area[0] = new Vector2(0, 1);
        multiplier = 1;
    }

    public Skill(Vector2[] a, int m)
    {
        area = a;
        multiplier = m;
    }

}
