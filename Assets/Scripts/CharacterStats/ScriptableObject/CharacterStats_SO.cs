using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Stats",menuName ="CharacterStats/Data")]
public class CharacterStats_SO : ScriptableObject
{
    [Header("Base Settings")]
    //最大生命值
    public int maxHealth;
    //当前生命值
    public int currHealth;
    //基础防御力
    public int baseDefence;
    //当前防御力
    public int currDefence;
    [Header("Kill")]
    public int killexp;
    public int killscore;
    [Header("Level")]
    public int currLevel;
    public int maxLevel;
    public int baseExp;
    public int currExp;
    public float levelBuff;
    public int currCoin;

    public void updateExp(int exp)
    {
        currExp += exp;
        if (currExp >= baseExp)
        {
            levelUp();
        }
    
    }
    public float LevelMultiper
    {
        get { return 1 + (currLevel - 1) * levelBuff; }
    }

    private void levelUp()
    {
        currLevel =Mathf.Clamp(currLevel + 1,0,maxLevel);

        baseExp =(int)(baseExp * LevelMultiper)+10;
        currExp = 0;
        maxHealth =maxHealth+20;
        currHealth = currHealth + 25>maxHealth?maxHealth:currHealth+25;
        Debug.Log("升级" + "currhealth:" + currHealth + "maxHealth:" + maxHealth + "currlevel" + currLevel);

    }
}
