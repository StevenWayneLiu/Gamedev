using UnityEngine;
using System.Collections;

public class EnemyStats : EntityStats {

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "Bullet")//if gameObject hit is a bullet
        {
            curHealth -= entity.GetComponent<BulletScript>().getDamage();//deal damage to player based on bullet's damage
            hpBar.fillAmount = (curHealth / maxHealth);
        }
    }
}
