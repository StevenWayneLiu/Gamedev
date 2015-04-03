using UnityEngine;
using System.Collections;

public class EnemyStats : EntityStats {

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        Hit(entity, "PlayerBullet");
    }
}
