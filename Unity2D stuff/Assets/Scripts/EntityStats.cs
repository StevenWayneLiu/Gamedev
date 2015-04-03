using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    public bool animate = true;
    public float curHealth;
    float maxHealth = 100;
    Image hpBar;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        hpBar = gameObject.GetComponentInChildren<Image>();
    }

    //defines behaviour when entity impacts other things
    public void Hit(GameObject entity, string tag)
    {
        if (entity.tag == tag)//if gameObject hit is a bullet
        {
            curHealth -= entity.GetComponent<BulletScript>().getDamage();//deal damage to player based on bullet's damage
            hpBar.fillAmount = (curHealth/maxHealth);
        }
            
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
