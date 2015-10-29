using UnityEngine;
using System.Collections;

public class FireBullet : Skill {

    public override void Use(Transform user)
    {
        base.Use(user);

        BulletPool bPool = BulletPool.bullets;
        bPool.Fire(user);
    }
}
