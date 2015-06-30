using UnityEngine;
using System.Collections;

public class mockupWeaponScript : MonoBehaviour {

    Animator anim;
    float dmg = 10f;//the amount of damage that this weapon does
	// Use this for initialization
	void Start () {
	    anim = gameObject.GetComponent<Animator>();
        tag = "Untagged";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            anim.SetBool("isAttacking", true);
        if(anim.GetBool("isAttacking"))
        {
            tag = "PlayerWeapon";
        }
	}
    public float getDamage()
    {
        if (anim.GetBool("isAttacking"))//returns damage value if character is in an attack animation
            return dmg;
        else
            return 0f;//otherwise return 0 and weapon does no damage
    }

}
