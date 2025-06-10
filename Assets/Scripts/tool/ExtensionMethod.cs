using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    private const float dotThreshold = 0.5f;
    public static bool isFacingTarget(this Transform transform, Transform target)
    {
        Vector3 vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();

        float dot = Vector3.Dot(transform.forward, vectorToTarget);

        return dot >= dotThreshold;


    }

    public static bool targetInAttackRange(this Transform transform, Transform target)
    {
        return Vector3.Distance(transform.position, target.position)
            <= transform.GetComponent<CharacterStats>().attackRange;

    }
    public static bool targetInSkillRange(this Transform transform, Transform target)
    {
        return Vector3.Distance(transform.position, target.position)
            <= transform.GetComponent<CharacterStats>().SkillRange;
    
    }
}
