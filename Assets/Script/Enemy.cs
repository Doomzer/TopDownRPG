using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    public float triggerLenght = 1;
    public float chaseLenght = 5;

    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTrasform;
    private Player player;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTrasform = GameManager.instance.player.transform;
        player = GameManager.instance.player;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!player.IsAlive())
            return;

        if (Vector3.Distance(playerTrasform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTrasform.position, startingPosition) < triggerLenght)
                chasing = true;

            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTrasform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantExp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " exp", 25, Color.magenta, transform.position, Vector3.up, 1f);
    }
}
