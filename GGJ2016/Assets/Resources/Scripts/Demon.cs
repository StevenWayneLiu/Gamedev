using UnityEngine;
using System.Collections;

public class Demon : Unit {

    public float eatTimer = 7f;
    public float eatSpeed = 5f;//time it takes for demon to devour character
    public float eatingRange = 3f;//distance that demon can eat from
    public bool isEating = false;
    public Transform target;//target demon is aggro'd onto
    public Collider eatCollider;

	// Use this for initialization
	void Start () {
        if(GameManager.instance.Followers > 0)
            target = GameObject.FindGameObjectWithTag("Follower").transform;//demon heads towards the camera
        value = 3;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //eating people
        if(target != null)
            Eat();

        //find a target nearby
        if (target == null)
        {
            if (GameManager.instance.units.Count > 0)
                target = GameObject.FindGameObjectWithTag("Follower").transform;
        }

        //always move towards target
        if(target != null)
            Move(target.position);
	}
    //when timer counts down to zero, kill target
    public void Eat()
    {
        if (isEating && target.tag == "Follower")
        {
            if (eatTimer >= 0)//if timer isn't up, tick down
                eatTimer -= Time.deltaTime;
            else//if timer is up target dies
            {
                target.GetComponent<Unit>().Death();
                target = null;
                isEating = false;
                eatTimer = eatSpeed;
                GameManager.instance.CheckGameOver();
            }
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other == target && other.tag == "Follower")
        {
            isEating = true;
        }
        else if (other.tag == "Follower" && !isEating)//switch targets
        {
            target = other.transform;
            isEating = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform == target)//target is out of range
        {
            isEating = false;
            eatTimer = eatSpeed;
        }
    }
}
