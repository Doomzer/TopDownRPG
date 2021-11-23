using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform XpBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenu;

    public int gold;
    public int exp;

    private void Awake()
    {
        //SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
        }
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
    }

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += gold.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponLevel + "|";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        if(player)
            player.transform.position = GameObject.Find("Spawn").transform.position;
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        gold = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        player.OnSetLevel(GetCurrentLv());
        weapon.weaponLevel = int.Parse(data[3]);
    }

    public bool TryUpWeapon()
    {
        if (IsWeponMax())
            return false;

        if (!IsEnoughtGold())
            return false;

        gold -= weaponPrices[weapon.weaponLevel];
        weapon.Upgrade();
        return true;
    }

    public bool IsWeponMax()
    {
        return weaponPrices.Count <= weapon.weaponLevel + 1;
    }

    public bool IsEnoughtGold()
    {
        return gold > weaponPrices[weapon.weaponLevel];
    }

    public int GetCurrentLv()
    {
        int level = 0;
        int expSum = 0;

        if (level == xpTable.Count)
            return level;

        while (exp >= expSum)
        {
            expSum += xpTable[level];
            level++;
            if (level == xpTable.Count)
                break;
        }

        return level;
    }

    public int GetXpToLv(int curLv)
    {
        int lv = 0;
        int expCur = exp;

        while (lv + 1 < curLv)
        {
            expCur -= xpTable[lv];
            lv++;
        }

        return expCur;
    }

    public void GrantExp(int newExp)
    {
        int curLv = GetCurrentLv();
        exp += newExp;
        int newLv = GetCurrentLv();
        if (curLv < newLv)
            for(int i = curLv; i < newLv; i++)
                OnLevelUp();
    }

    void OnLevelUp()
    {
        player.OnLevelUp();
    }

    public void OnXpChange()
    {
        float ratio = 0;
        if (player.hitpoint > 0)
        {
            ratio = (float)player.hitpoint / player.maxHitpoint;
        }
        XpBar.localScale = new Vector3(1, ratio, 1);
    }

    public void Restart()
    {
        deathMenu.ResetTrigger("Show");
        deathMenu.SetTrigger("Hide");
        SceneManager.LoadScene("MainScene");
        player.Respawn();
    }
}
