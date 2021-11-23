using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.OnXpChange();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
               
        moveDelta = new Vector2(x, y);
        if(isAlive)
            UpdateMotor(moveDelta);
    }

    public void SwapSprite(int current)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[current];
    }

    public void OnLevelUp()
    {
        maxHitpoint += 2;
        hitpoint = maxHitpoint;
        GameManager.instance.OnXpChange();
    }

    public void OnSetLevel(int level)
    {
        if (level > 1)
        {
            maxHitpoint = 10;
            for (int i = 1; i <= level; i++)
                OnLevelUp();
        }
    }

    public bool Heal(int amount)
    {
        if (hitpoint == maxHitpoint)
            return false;

        hitpoint += amount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;

        GameManager.instance.ShowText("+" + amount + " xp", 25, Color.green, transform.position, Vector3.up, 1f);
        GameManager.instance.OnXpChange();
        return true;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (isAlive)
        {
            base.ReceiveDamage(dmg);
            GameManager.instance.OnXpChange();
        }
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenu.SetTrigger("Show");
    }

    public void Respawn()
    {
        isAlive = true;
        hitpoint = maxHitpoint;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
