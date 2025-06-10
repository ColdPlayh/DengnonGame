using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour,IEndGameObserver
{
    protected EnemyStates enemyStates;
    protected CharacterStats characterStats;
    private NavMeshAgent agent;
    private Animator anim;
    public Material[] lightMaterial;
        
    

    [Header("Base Setting")]

    public float sightRadius;
    public float speed;
    public float waittime;
    public float stoptime;
    public float afterchasewaittime;
    public float patrolRadius;
    public bool isguard;
    
    [Header("Skill Setting")]
    public bool hasskill;
    public float skillchance;
    [Header("fall Setting")]
    public bool isfall;
    public GameObject coinPrefab;
    public float fallchance;
    public int mincoin;
    public int maxcoin;
    public float maxfallradius;
    public float minfallradius;
    private bool fallcomplete;
    
    public enum fallType
    { 
        A,B,C
    }

    
    protected GameObject attacktarget;


    
    private float stepwaittime;
    private float stepACWT;
    private float lastattacktime;
    private Vector3 guardCenter;
    //储存站桩怪默认的朝向
    private Quaternion guardRotation;
    private Vector3 wayPoint;
    private Collider coll;

    private bool iswalk;
    private bool ischase;
    private bool isrun;
    private bool isdead=false;
    private bool iswin;
    public GameObject AttackTarget
    {
        get { if (attacktarget != null) return attacktarget; else return null; }
        set { if (attacktarget != null) attacktarget = value; }
    }
    public virtual void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        coll = GetComponent<Collider>();

        agent.speed = speed;
        guardCenter = transform.position;
        guardRotation = transform.rotation;
        stepwaittime = waittime;
        lastattacktime = 0;
        fallcomplete = false;
        stepACWT = afterchasewaittime;
       
        
    }
    
    private void OnEnable()
    {


        
       // if (!GameManager.isInitialized())
          //  GameManager.Instance.rigisterObserver(this);
    }
    public void Start()
    {
        if (isguard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else {

            enemyStates = EnemyStates.PATROL;
        }
        init();
        //Debug.Log(enemyStates);
        if (!GameManager.isInitialized())
            GameManager.Instance.rigisterObserver(this);

    }
    private void OnDisable()
    {
        if (!GameManager.isInitialized()) return;
        GameManager.Instance.removeObserver(this);
    }
    public void Update()
    {
        if(GameManager.isInitialized()&&!GameManager.Instance.containsObserver(this))
            GameManager.Instance.rigisterObserver(this);
        checkHealth();
        switchState();
        switchAnimation(); 
        
    }
    public void switchAnimation()
    {
        anim.SetBool("isWalk", iswalk);
        anim.SetBool("isChase", ischase);
        anim.SetBool("isRun", isrun);
        anim.SetBool("isDead", isdead);
        anim.SetBool("isWin", iswin);

    }
    public void switchState()
    {
        if (isdead)
        {
            enemyStates = EnemyStates.DEAD;
           
            //Debug.Log("进入死亡状态");
        }
        if (foundPlayer() && !isdead)
        {
            enemyStates = EnemyStates.CHASE;
        }
            
        switch (enemyStates)
        {
            case EnemyStates.PATROL:
                patrolState();
                break;
            case EnemyStates.CHASE:
                chaseState();
                break;
            case EnemyStates.DEAD:
                deadState();
                break;
            case EnemyStates.GUARD:
                guardState();
                break;

        }
           
    }
    public void guardState()
    {
        //设置不在追击
        ischase = false;
        //如果不在守卫地点
        if (transform.position != guardCenter)
        {
            //用走动的方式
            iswalk = true;
            //agent开启
            agent.isStopped = false;
            //设置目的地
            agent.destination = guardCenter;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
            //如果到达目的地
            if (Vector3.SqrMagnitude(guardCenter - transform.position) <= agent.stoppingDistance)
            {
                //旋转到最初的角度
                transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
                //不再播放走动的动画
                iswalk = false;
            }
        }

    }
    public void patrolState()
    {
        ischase = false;
        agent.speed = speed * 0.5f;
        if (Vector3.Distance(transform.position, wayPoint) <= agent.stoppingDistance)
        {
            iswalk = false;
            if (stepwaittime > 0)
            {
                stepwaittime -= Time.deltaTime;
            }
            else
            {
                stepwaittime = waittime;
                createWayPoint();
            }
        }
        else
        {
            iswalk = true;
            stoptime += Time.deltaTime;
            agent.SetDestination(wayPoint);
            if (stoptime > 5f)
            {
                createWayPoint();
                stoptime = 0;
            }


        }

    }
    public void chaseState()
    {
        iswalk = false;
        ischase = true;
        
        agent.speed = speed;
        
        if (!foundPlayer())
        {
            isrun = false;
            if (stepACWT > 0)
            {
                stepACWT = stepACWT - Time.deltaTime;
                agent.SetDestination(transform.position);
            }
            else
            {
                
                if (isguard)
                {
                    //重新设置等待cd
                    stepACWT = afterchasewaittime;
                    //进入站桩状态
                    enemyStates = EnemyStates.GUARD;

                }
                else
                {
                    //进入巡逻状态
                    stepACWT = afterchasewaittime;
                    enemyStates = EnemyStates.PATROL;
                }

            }

        }
        else {
            isrun = true;
            //agent.destination = attacktarget.transform.position;
            agent.SetDestination(attacktarget.transform.position);

            agent.isStopped = false;
            if (targetInAttackRange() || targetInSkillRange())
            {
                isrun = false;
                agent.isStopped = true;
                if (lastattacktime <= 0)
                {
                    
                    lastattacktime = characterStats.attackCd;
                    characterStats.isCritical = UnityEngine.Random.value <= characterStats.criticalChance;
                    //攻击的方法
                    if (hasskill)
                    {
                        attackOne();
                    } 
                    else
                    {
                        attackTwo();

                    }
                        
                }
                else {
                    lastattacktime -= Time.deltaTime;
                }
            }

        }
    }
    protected virtual void deadState()
    {
        attacktarget = null;
        //碰撞器关闭 人物不能在攻击
        coll.enabled = false;
        //agent关闭
        agent.isStopped = true;
        GameManager.Instance.MaxFallRadius = maxfallradius;
        GameManager.Instance.MinFallRadius = minfallradius;
        float temp = UnityEngine.Random.Range(0,1f);
        
        if (isfall &&  temp<= fallchance)
        {
            if (!fallcomplete)
            {
                Debug.Log(temp + "掉落" + gameObject.name);
                int count = UnityEngine.Random.Range(mincoin, maxcoin);
                for (int i = 0; i < count; i++)
                {
                    if (coinPrefab != null)
                    {
                        Instantiate(coinPrefab,
                       new Vector3(transform.position.x, transform.position.y, transform.position.z),
                       Quaternion.Euler(90, 0, 0));
                    }
                }
            }
            fallcomplete = true;
        }
        
        Destroy(gameObject, 2.5f);
       
    }

    void attackTwo()
    {
        transform.LookAt(attacktarget.transform);
        if (targetInAttackRange())
        {
            //Debug.Log("近战攻击");
            //近战攻击
            anim.SetTrigger("Attack");
        }

    }
    void attackOne()
    {
        float chance = UnityEngine.Random.value;
        //面向玩家
        transform.LookAt(attacktarget.transform);
        //执行近战攻击
       // Debug.Log("chance："+chance);
        if ( chance > skillchance && targetInAttackRange())
        {
            Debug.Log("近战攻击");
            //近战攻击
            anim.SetTrigger("Attack");
            

        }
        //执行远程攻击
        else if (chance<skillchance&&targetInSkillRange())
        {
            //远程攻击
            Debug.Log("执行技能");
            anim.SetTrigger("Skill");
        }

    }
    public void AttackHit()
    {
        if (attacktarget != null
            && transform.targetInAttackRange(attacktarget.transform)
            && transform.isFacingTarget(attacktarget.transform))
        {
            var targetState = attacktarget.GetComponent<CharacterStats>();
            characterStats.takeDamage(characterStats, targetState, Constant.DAMAGE_TYPE_ATTACK,false,0);
            
        }
    }
    void checkHealth()
    {
        if (characterStats.currHealth <= 0)
        {
            //设置isdead为true 播放死亡动画
            isdead = true;
            //Debug.Log("敌人死亡");
        }
    }
    bool foundPlayer()
    {
        
        //用一个收集器 在视线范围内所有物体都会进入到var里面 var可以存任何类型object
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        //遍历
        foreach (var target in colliders)
        {
            //如果有玩家
            if (target.CompareTag("Player"))
            {
                //设置攻击目标
                attacktarget = target.gameObject;
                //返回true
                return true;
            }
        }
        return false;
    }
    private void createWayPoint()
    {
        float wayX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        float wayZ = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        Vector3 newWayPoint = new Vector3(guardCenter.x + wayX, transform.position.y, guardCenter.z + wayZ);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(newWayPoint, out hit, patrolRadius, 1) ? newWayPoint : transform.position;
    }
    public bool targetInAttackRange()
    {
        if (attacktarget != null)
        {
            return Vector3.Distance(transform.position, attacktarget.transform.position) <= characterStats.attackRange;
        }
        else {
            return false;
        }
    }
    bool targetInSkillRange()
    {
        if (attacktarget != null)
        {
            
            //触发技能
            return Vector3.Distance(transform.position, attacktarget.transform.position) <= characterStats.SkillRange;
        }
        else
        {
            return false;
        }
    }
    public void stopAnyBehavior()
    {
        iswalk = false;
        ischase = false;
        isrun= false;
        isdead = false;
        attacktarget = null;
        //其实这里不需要设置 因为没有了攻击目标我们的敌人不会再攻击
        agent.isStopped = true;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawWireSphere(transform.position, sightRadius);
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(transform.position, 2.5f);
        // Gizmos.DrawWireSphere(transform.position, 3f);
        
        
    }
    public void OnDrawGizms()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, wayPoint);
    }


    public void init()
    {
        //Debug.Log("初始化");
        iswalk = false;
        ischase = false;
        isrun = false;
        isdead = false;
        iswin = false;
        attacktarget = null;
        agent.isStopped = false;
        agent.destination = transform.position;
        createWayPoint();
    }
    public virtual void endGameNotify()
    {
        
        stopAnyBehavior();
        iswin = true;
        
    }
    public virtual void OnDestroy()
    {
        
    }

}
