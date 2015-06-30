using UnityEngine;
using System.Collections;

public class EnemyStats : EntityStats {


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Weapon hit enemy");
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "PlayerWeapon")//if gameObject hit is a bullet
        {
            curHealth -= entity.GetComponent<mockupWeaponScript>().getDamage();//deal damage to player based on bullet's damage
            hpBar.fillAmount = (curHealth / maxHealth);
        }
    }
}
