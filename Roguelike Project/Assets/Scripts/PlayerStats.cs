using UnityEngine;
using System.Collections;

public class PlayerStats : EntityStats {
    public Canvas deathCanvas;
    public override void Death()
    {
        //go to game-over screen
        deathCanvas.enabled = true;
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Entity")//if gameObject hit is a bullet
        {
            curHealth -= entity.GetComponent<EnemyStats>().touchDmg;//deal damage to player based on bullet's damage
            hpBar.fillAmount = (curHealth / maxHealth);
        }
        if(entity.tag == "Bullet")
        {
            curHealth -= entity.GetComponent<BulletScript>().getDamage();//deal damage to player based on bullet's damage
            hpBar.fillAmount = (curHealth / maxHealth);
        }
    }
}
