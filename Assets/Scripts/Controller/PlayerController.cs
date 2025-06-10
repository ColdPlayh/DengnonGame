using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerController : SingleTon<PlayerController>
{
    [Header("JoyStickSetting")]
    public Joystick myJoystick;

    [Header("BaseSetting")]
    public float attackRange;
    public float skillRange;
    public float speed;
    public float rollspeed;
    public CharacterStats characterStats;

    Animator anim;
    private CharacterController character;
    private bool isRun;
    private float horizontalMove;
    private float verticalMove;
    private GameObject[] inRangeEnemys;
    private List<GameObject> attackenemys;
    private int attacktype;
    private float lastattacktime;
    private bool ismove;
    private bool isroll;
    private bool isinv;
    private bool isdead;


    public bool IsMove

    {
        set { ismove = value; }
    }
    public bool IsRoll

    {
        set { isroll = value; }
    }
    public bool IsInv
    {
        get { return isinv; }
        set { isinv = value; }
    }
    void OnEnable()
    {
        characterStats = GetComponent<CharacterStats>();
        GameManager.Instance.rigisterPlayer(characterStats);
        
        if (GameManager.Instance != null&&GameManager.Instance.myJoystick!=null)
        {
            myJoystick = GameManager.Instance.myJoystick;
        }
        else
        {
            myJoystick = FindObjectOfType<VariableJoystick>();


        }
        
        
    }
   override protected void Awake()
    {
        base.Awake();
        Debug.Log("启用");
        characterStats = GetComponent<CharacterStats>();
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        attackenemys = new List<GameObject>();
        lastattacktime = 0;
        ismove = true;
        
    }
    void Start()
    {
        //注册到ganmeManager
       
       
        SaveManager.Instance.loadPlayerData();
       
    }
    
    private void Update()
    {
        if (myJoystick == null)
        {
            myJoystick = FindObjectOfType<VariableJoystick>();
           
            GameManager.Instance.rigisterJoyStick(myJoystick);
        }
        timer();
        //人物控制
        playerMove();
        //动画切换
        switchAnim();
        switchHealth();
    }
    public void switchHealth()
    {
        if (gameObject == null)
            return;
        if (characterStats.currHealth == 0)
        {

            isdead = true;
            GameManager.Instance.notifyObserver();
        }
    }

    public void switchAnim()
    {
        anim.SetBool("isRun", isRun);
        anim.SetBool("isDead", isdead);
    }

    public void playerMove()
    {
        if (isdead) return;
        if (!ismove)
        {
            if (isroll)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * rollspeed);
            }
            return;
        }
        
        if (myJoystick.Horizontal != 0 || myJoystick.Vertical != 0)
        {
            isRun = true;
            horizontalMove = myJoystick.Horizontal * speed;
            verticalMove = myJoystick.Vertical * speed;
            transform.forward = new Vector3(myJoystick.Horizontal, 0, myJoystick.Vertical);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
          
        }
        else 
        {
            isRun = false;
            
        }      
    }
    public void timer()
    {
       if (isdead) return;
       if (lastattacktime>0)
       {
            lastattacktime -= Time.deltaTime;
       }
    }
    public void attack()
    {
        if (isdead) return;
        if (lastattacktime > 0)
        {
            Debug.Log("cd中");
            return;
        }
        else {
            anim.SetTrigger("Attack");
            attackenemys = enemysInAttackRange();
            attacktype = Constant.DAMAGE_TYPE_ATTACK;
            lastattacktime = characterStats.attackCd;
        }
        
        
    }
    public void skill()
    {
        if (isdead) return;
        anim.SetTrigger("Skill");
        attackenemys=enemysInSkillRange();
        attacktype = Constant.DAMAGE_TYPE_SKILL;


    }

    public void roll()
    {
        if (isdead) return;
        anim.SetTrigger("Roll");
    }
    public void AttackHit()
    {
        if (isdead) return;
        if (attackenemys != null && attackenemys.ToArray().Length > 0)
        {
            Debug.Log("进行攻击");
            foreach (var enemy in attackenemys)
            {
                if (enemy != null)
                {
                    var targetStats = enemy.GetComponent<CharacterStats>();
                    characterStats.takeDamage(characterStats, targetStats, attacktype,true,7f);
                }
            }
        }
            
    }
    public List<GameObject> enemysInAttackRange()
    {
        
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < enemys.Length; i++)
        {
            
            float dis = Vector3.Distance(transform.position, enemys[i].transform.position);
            
            if (dis < characterStats.attackRange)
            {
                
                float angle = Vector3.Angle(transform.forward, 
                    enemys[i].transform.position - transform.position);
                
                if (angle < characterStats.AttackAngle)
                {
                    result.Add(enemys[i]);
                }
            }
        }
        if (result.ToArray().Length>0)
            Debug.Log("敌人在攻击范围内" + "  数量:" + result.ToArray().Length);
        return result;
        
    }

    public List<GameObject> enemysInSkillRange()
    {
        
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> result=new List<GameObject>();
        
        for(int i=0;i<enemys.Length;i++)
        {
            float dis = Vector3.Distance(transform.position, enemys[i].transform.position);           
            if (dis < characterStats.SkillRange)
            {

                result.Add(enemys[i]);
            }
        }
        if (result.ToArray().Length > 0)
            Debug.Log("敌人在技能范围内" + "  数量:" + result.ToArray().Length);
        return result;
    }
    //TODO:翻滚ai
    public void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }

}
