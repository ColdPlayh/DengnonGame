using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("Skill")]
    public float kickForce = 5;
    public void Hit()
    {
        if (attacktarget != null && transform.isFacingTarget(attacktarget.transform))
        {
            characterStats.takeDamage(characterStats, attacktarget.GetComponent<CharacterStats>(),
                Constant.DAMAGE_TYPE_ATTACK, false, 0);
        }
    }
    public void Skill()
    {
        if (attacktarget != null
            && transform.targetInSkillRange(attacktarget.transform))
        {
            attacktarget.GetComponent<Animator>().SetTrigger("Dizzy");
            characterStats.takeDamage(characterStats, attacktarget.GetComponent<CharacterStats>(),
                Constant.DAMAGE_TYPE_SKILL, true, kickForce);

        }

    }
    protected override void deadState()
    {
        base.deadState();
        WinManager.Instance.show();
        PlayerController.Instance.IsMove = true;
    }
    public override void endGameNotify()
    {
        base.endGameNotify();    
    }

}