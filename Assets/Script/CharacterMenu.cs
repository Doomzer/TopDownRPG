using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText;
    public Text hitpointText;
    public Text goldText;
    public Text upgradeText;
    public Text expText;

    private int currentChar = 0;
    public Image charSprite;
    public Image weaponSprite;
    public RectTransform expBar;

    public void OnClickArrow(bool right)
    {
        if (right)
        {
            currentChar++;
            if (currentChar == GameManager.instance.playerSprites.Count)
                currentChar = 0;
        }
        else
        {
            currentChar--;
            if (currentChar < 0)
                currentChar = GameManager.instance.playerSprites.Count - 1;
        }
        OnCharChange();
    }

    private void OnCharChange()
    {
        charSprite.sprite = GameManager.instance.playerSprites[currentChar];
        GameManager.instance.player.SwapSprite(currentChar);
    }

    public void OnClickUpgrade()
    {
        if (GameManager.instance.TryUpWeapon())
            UpdateMenu();
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        upgradeText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        if (GameManager.instance.IsWeponMax())
        {
            upgradeText.text = "MAX";
            upgradeText.GetComponentInParent<Image>().color = Color.gray;
        }
        else if (GameManager.instance.IsEnoughtGold())
        {
            upgradeText.GetComponentInParent<Image>().color = Color.green;
        }
        else
        {
            upgradeText.GetComponentInParent<Image>().color = Color.red;
        }

        int level = GameManager.instance.GetCurrentLv();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + "/" + GameManager.instance.player.maxHitpoint.ToString();
        levelText.text = level.ToString();
        goldText.text = GameManager.instance.gold.ToString();

        if (level == GameManager.instance.xpTable.Count)
        {
            expText.text = "MAX";
            expBar.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            int expCur = GameManager.instance.GetXpToLv(level);
            int expNeedToLv = GameManager.instance.xpTable[level - 1];
            expText.text = expCur.ToString() + "/" + expNeedToLv.ToString();
            float bar = (float)expCur / expNeedToLv;
            expBar.localScale = new Vector3(bar, 1, 1);
        }
    }
}
