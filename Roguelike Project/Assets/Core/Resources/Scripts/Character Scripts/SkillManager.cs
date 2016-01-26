using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//allows game object to use skills
public class SkillManager : MonoBehaviour {

    public Inventory<Skill> skills = new Inventory<Skill>();//holds skills that entity can use
    float timer = 0;//count time for duration
    public bool getTargs = false;//determines when to start checking for targets
    List<GameObject> targ;//targets for direct targeting

    public void Start()
    {
        
    }

    void Update()
    {
        //check input, activate skill based on what skill is assigned to what input
        if(Input.GetButtonDown("Fire1"))
        {
            if(((Skill)skills[0]).collider)//if using colliders to find targets
            {
                getTargs = true;
            }
            else if(((Skill)skills[0]).cast)//if using raycasts to find targets
            {
                //do raycast or shapecast or linecast or whatever
                ((Skill)skills[0]).Use();//use skill on targets
            }
            else//if using direct targeting to find targets
            {
                //get target through whatever means
                //give target to skill
                ((Skill)skills[0]).Use();//use skill on targets
            }
        }
        if (getTargs)
        {
            timer += Time.deltaTime;
            if (timer == ((Skill)skills[0]).duration)//reset when duration is up
            {
                getTargs = false;
                timer = 0;
                ((Skill)skills[0]).Use();//use skill on targets
            }
            
        }
            

    }

    void UseSkill()
    {
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (getTargs && ((Skill)skills[0]).collider)//when colliding with something, give selected skill a target
        {
            if(other.GetComponentInChildren<Character>() != null)
            {
                ((Skill)skills[0]).targets.Add(other.GetComponentInChildren<Character>());
            }
        }
    }

}
