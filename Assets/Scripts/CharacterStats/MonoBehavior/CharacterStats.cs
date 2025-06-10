using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO templateData;
    //控制基本属性
    public CharacterStats_SO characterStateSo;
    //控制攻击属性
    public CharacterATK_SO templateAtkData;
    public CharacterATK_SO characterATKStateSo;
    public bool isCritical = false;
    private GameObject tempattacktarget;
    private void Awake()
    {
        if (templateData != null)
        {
            characterStateSo = Instantiate(templateData);
        }
        if (templateAtkData != null)
        {
            characterATKStateSo = Instantiate(templateAtkData);
        }
    }
    public void takeDamage(CharacterStats attacker, CharacterStats defenser, int damageType, bool iskickoff, float kickforce)
    {
        if (defenser.currHealth <= 0)
        {
            return;
        }
        var defenserobj = defenser.gameObject;
        if (defenserobj.CompareTag("Player") && defenserobj.GetComponent<PlayerController>().IsInv)
        {
            Debug.Log("闪避成功");
            return;
        }
        var attackerobj = attacker.gameObject;
        int damage = Mathf.Max(attacker.currDamage(damageType) - defenser.currDefence, 1);
        defenser.currHealth = Mathf.Max(defenser.currHealth - damage, 0);
        Debug.Log(defenser.tag + ":" + defenser.currHealth);

        if (iskickoff && defenser.currHealth > 0)
        {
            Debug.Log("击退");
            kickOff(attackerobj, defenserobj, kickforce);

        }
        else if (defenser.currHealth <= 0)
        {
            attacker.characterStateSo.updateExp(defenser.characterStateSo.killexp);
            GameManager.Instance.GameScore += defenser.characterStateSo.killscore;
        }
       
        
    }
    public void kickOff(GameObject attacker,GameObject defenser ,float kickforce)
    {
        float kickForce = kickforce;
        Debug.Log("kickforce" + kickForce);
        Vector3 direction = defenser.transform.position - attacker.transform.position;
        direction.Normalize();
        defenser.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
        
       
       
        //defenser.GetComponent<NavMeshAgent>().isStopped = true;
        //StartCoroutine(WaitForSeconds(1f,()=>
        //    {
        //        defenser.GetComponent<NavMeshAgent>().isStopped = false;
        //    }
        //    ));
        
        
    }
    public  IEnumerator WaitForSeconds(float waittime,Action action=null)
    {
        yield return new WaitForSeconds(waittime);
        action?.Invoke();
    }

    public int currDamage(int damageType)
    {
        float result = UnityEngine.Random.Range(minDamge, maxDamge);
        if (damageType == Constant.DAMAGE_TYPE_SKILL)
        {
            result = result * SkillMutipler;
        }
        if(isCritical)
        {
            result = result * criticalMultipler;
        }
        return (int)result;
    }
    public int maxHealth
    {
        get { if (characterStateSo != null) return characterStateSo.maxHealth; else return -1; }
        set { if (characterStateSo != null) characterStateSo.maxHealth = value; }
    }
    public int currHealth
    {
        get { if (characterStateSo != null) return characterStateSo.currHealth; else return -1; }
        set { if (characterStateSo != null) characterStateSo.currHealth = value; }
    }
    public int baseDefence
    {
        get { if (characterStateSo != null) return characterStateSo.baseDefence; else return -1; }
        set { if (characterStateSo != null) characterStateSo.baseDefence = value; }
    }
    public int currDefence
    {
        get { if (characterStateSo != null) return characterStateSo.currDefence; else return -1; }
        set { if (characterStateSo != null) characterStateSo.currDefence = value; }
    }
    public float attackRange
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.attackRange; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.attackRange = value; }
    }
    public float SkillRange
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.skillRange; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.skillRange = value; }
    }
    public float attackCd
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.attackCd; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.attackCd = value; }
    }

    public int minDamge
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.minDamge; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.minDamge = value; }
    }
    public int maxDamge
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.maxDamge; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.maxDamge = value; }
    }
    public float criticalMultipler
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.CriticalMultipler; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.CriticalMultipler = value; }
    }
    public float criticalChance
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.CriticalChance; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.CriticalChance = value; }

    }
    public float AttackAngle
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.attackAngle; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.attackAngle = value; }
    }
    public float SkillMutipler
    {
        get { if (characterATKStateSo != null) return characterATKStateSo.skillMutipler; else return -1; }
        set { if (characterATKStateSo != null) characterATKStateSo.skillMutipler = value; }
    }

}
