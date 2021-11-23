using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldMin = 10;
    public int goldMax = 100;

    protected override void OnCollect()
    {
        if(!collected)
        {
            int gold = Random.Range(goldMin, goldMax);
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += gold;
            GameManager.instance.ShowText("+" + gold + " gold", 25, Color.yellow, transform.position, Vector3.up * 5, 3.0f);
        }
    }
}
