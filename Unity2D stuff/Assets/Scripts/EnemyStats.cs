using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    public bool animate = true;
    int hp = 100;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            GameObject.Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        if (entity.tag == "PlayerBullet")//if gameObject hit is a bullet
            hp -= entity.GetComponent<BulletScript>().getDamage();//deal damage to player based on bullet's damage
    }
}
