using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float pushForce = 1;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                pushForce = pushForce,
                origin = transform.position
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
