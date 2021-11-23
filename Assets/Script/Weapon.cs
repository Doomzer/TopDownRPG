using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = { 1, 2, 3, 4, 5 };
    public float[] pushForce = { 2.3f, 2.6f, 3.0f, 3.5f, 4.0f };

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    private Animator anim;
    private float cooldown = 0.3f;
    private float lastSwing;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();        
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name != "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                pushForce = pushForce[weaponLevel],
                origin = transform.position
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void Upgrade()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
