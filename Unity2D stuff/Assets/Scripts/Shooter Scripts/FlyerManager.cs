using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyerManager : CharacterManager {

    public bool animate = true;
    public float touchDmg = 0;//amount of damage done to entities this entity touches
    public Image hpBar;
    public Canvas deathCanvas;
    public int[] skillList;//holds indexes of usable skills from global skill list

    void Start()
    {
        base.Start();
        if(GetComponentInChildren<Image>())
            hpBar = gameObject.GetComponentInChildren<Image>();
    }

    //defines behavior when entity's health drops to zero
    public virtual void Death()
    {
        GameObject.Destroy(gameObject);
        if(gameObject.tag == "Player")//use gameover screen if player dies
        {
            if(deathCanvas)
                deathCanvas.enabled = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (charInfo.CurHealth <= 0)//check for death
            Death();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Entity" || entity.tag == "Player")//if gameObject hit is a bullet
        {
            charInfo.CurHealth -= entity.GetComponent<CharacterManager>().charInfo.Strength;//deal damage to player based on bullet's damage
        }
        if (entity.tag == "Bullet")
        {
            charInfo.CurHealth -= entity.GetComponent<BulletScript>().getDamage();//deal damage to player based on bullet's damage
        }
        if(hpBar)
            hpBar.fillAmount = (charInfo.HealthFract);
    }
}
