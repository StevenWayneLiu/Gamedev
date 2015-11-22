using UnityEngine;
using System.Collections;

public class BulletManager : CharacterManager {

    float duration = 2f;//how long the bullet lasts before terminating

    void Start()
    {
        base.Start();
        className = "Bullet";
        charInfo.Strength = 5;
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * 15f;
        Invoke("Deactivate", duration);//deactivate bullet after a certain time has passed
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)//do stuff when bullet hits target
    {
        GameObject entity = collider.gameObject;//find gameObject for collider hit
        CharacterManager otherChar = entity.GetComponent<CharacterManager>();
        if (entity.tag != gameObject.tag && otherChar)//if gameObject hit isn't the same faction as the bullet
        {
            if(entity.GetComponent<CharacterManager>().className != "Bullet")//if gameobject isn't a bullet
            {
                otherChar.charInfo.CurHealth -= charInfo.Strength;//take damage from bullet
                Deactivate();//deactivate bullet
            }
        }
    }
}
