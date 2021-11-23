using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : Collidable
{
    public int healingValue = 1;
    public float healCooldown = 1f;
    private float lastHeal;


    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            if (Time.time - lastHeal > healCooldown)
            {
                if (GameManager.instance.player.Heal(healingValue))
                    lastHeal = Time.time;
            }
        }
    }
}
