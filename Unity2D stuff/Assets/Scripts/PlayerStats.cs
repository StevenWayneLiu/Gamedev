using UnityEngine;
using System.Collections;

public class PlayerStats : EntityStats {
	
    public override void Death()
    {
        //go to game-over screen
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        Hit(entity, "Enemy");
    }
}
