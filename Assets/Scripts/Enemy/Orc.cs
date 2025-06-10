using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc : EnemyController
{
    [Header("Orc Setting")]
    public float kickforce;
    public GameObject portalPreFab;
    public bool isCreate;
    GameObject createPoint;
    private bool protalcomplelte;


    public override void Awake()
    {
        base.Awake();
        protalcomplelte = false;
    }
    public void Hit()
    {
        if (transform.targetInAttackRange(attacktarget.transform)
           && transform.isFacingTarget(attacktarget.transform)
           && attacktarget != null)
        {
            characterStats.takeDamage(characterStats, attacktarget.GetComponent<CharacterStats>(),
                Constant.DAMAGE_TYPE_ATTACK,false,0);
        
        }
    
    }
    public void Skill()
    {
        if (attacktarget != null
            && transform.targetInSkillRange(attacktarget.transform))
        {
            attacktarget.GetComponent<Animator>().SetTrigger("Dizzy");
            characterStats.takeDamage(characterStats, attacktarget.GetComponent<CharacterStats>(),
                Constant.DAMAGE_TYPE_SKILL, true, kickforce);
            
        }
    
    }
    protected override void deadState()
    {
        base.deadState();
        if (portalPreFab != null&&!protalcomplelte)
        {
            createPoint = GameObject.FindGameObjectWithTag("create portal");
            Instantiate(portalPreFab, createPoint.transform.position,
                Quaternion.Euler(new Vector3(-90, 0, 0)));
            protalcomplelte = true;

        }
    }
    public override void OnDestroy()
    {
        
        
    }
}
