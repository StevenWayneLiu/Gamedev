using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyerManager : CharacterManager {

    public bool animate = true;
    public float touchDmg = 0;//amount of damage done to entities this entity touches
    public Image hpBar;
    public Canvas deathCanvas;
    public int[] skillList;//holds indexes of usable skills from global skill list
    FlyerController2D cont;

    void Start()
    {
        base.Start();
        className = "Flyer";
        charInfo.Strength = 5;
        if(GetComponentInChildren<Image>())
            hpBar = gameObject.GetComponentInChildren<Image>();

        if (GetComponentInChildren<FlyerController2D>())
            cont = gameObject.GetComponentInChildren<FlyerController2D>();
        else
            if(gameObject.tag == "Player")
                cont = gameObject.AddComponent<FlyerController2D>();
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
        else
        {
            GameData.data.Scores["Kills"]++;
            Destroy(gameObject);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (charInfo.CurHealth <= 0)//check for death
            Death();
        hpBar.fillAmount = (charInfo.HealthFract);
    }
    
    //only enemies and bullets call this, player character has non-trigger collider
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag != gameObject.tag && (entity.tag == "Player" || entity.tag == "Enemy"))
        {
            entity.GetComponent<CharacterManager>().charInfo.CurHealth -= charInfo.Strength;//deal damage from touching
        }
        
    }
}
