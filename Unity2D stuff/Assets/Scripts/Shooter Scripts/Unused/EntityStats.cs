using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    public bool animate = true;
    public float curHealth = 100;
    public float maxHealth = 100;
    public float touchDmg = 0;//amount of damage done to entities this entity touches
    public Image hpBar;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        hpBar = gameObject.GetComponentInChildren<Image>();
    }
    //defines behavior when entity's health drops to zero
    public virtual void Death() 
    {
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
            Death();
    }

}
