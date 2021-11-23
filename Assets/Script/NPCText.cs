using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;
    private float cooldown = 4f;
    private float lastText;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            if (Time.time - lastText > cooldown || lastText == 0)
            {
                Vector3 pos = transform.position + new Vector3(0, 0.16f, 0);
                GameManager.instance.ShowText(message, 25, Color.white, pos, Vector3.zero, 4f);
                lastText = Time.time;
            }
        }        
    }
}
