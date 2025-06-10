using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Attack",menuName ="CharacterAttack/Data")]
public class CharacterATK_SO : ScriptableObject
{
    //近战范围
    public float attackRange;
    //技能范围
    public float skillRange;
    //攻击cd
    public float attackCd;
    public float skillCd;
    public float attackAngle;
    public float skillMutipler;
    //最小伤害
    public int minDamge;
    //最大伤害
    public int maxDamge;
    //暴击伤害
    public float CriticalMultipler;
    //暴击率
    public float CriticalChance;

}


