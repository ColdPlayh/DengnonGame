using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthManager : SingleTon<StrengthManager>
{
    Button exitBtn;
    Button sthAttackBtn;
    Button sthDefenceBtn;
    Button sthHealthBtn;
    Text sthAttackCost;
    Text sthDefenceCost;
    Text sthHealthCost;
    public int atkcost;
    public int defcost;
    public int helcost;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        
        exitBtn = transform.GetChild(0).GetComponent<Button>();
        sthAttackBtn= transform.GetChild(1).GetChild(0).GetComponent<Button>();
        sthDefenceBtn= transform.GetChild(2).GetChild(0).GetComponent<Button>();
        sthHealthBtn = transform.GetChild(3).GetChild(0).GetComponent<Button>();
        sthAttackCost= transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        sthDefenceCost = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        sthHealthCost = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>();
        init();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
    public void sthAttack()
    {
        GameManager.Instance.playerStats.minDamge+= 1;
        GameManager.Instance.playerStats.maxDamge += 1;
        GameManager.Instance.playerStats.characterStateSo.currCoin -= atkcost;
        atkcost += 10;
        updateUI();
    }
    public void sthDefence()
    {

        GameManager.Instance.playerStats.currDefence += 1;    
        GameManager.Instance.playerStats.characterStateSo.currCoin -= defcost;
        defcost += 10;
        updateUI();
        //Debug.Log(GameManager.Instance.playerStats.currDefence + ":" + defcost);
    }
    public void sthHealth()
    {
        GameManager.Instance.playerStats.currHealth += 10;
        GameManager.Instance.playerStats.maxHealth += 10;
        GameManager.Instance.playerStats.characterStateSo.currCoin -= helcost;
        helcost += 10;
        updateUI();
        //Debug.Log(GameManager.Instance.playerStats.currHealth + ":" +
         //   GameManager.Instance.playerStats.maxHealth + ":"+ helcost);
    }
    public void init()
    {
        
        sthAttackBtn.onClick.AddListener(sthAttack);
        sthDefenceBtn.onClick.AddListener(sthDefence);
        sthHealthBtn.onClick.AddListener(sthHealth);
        exitBtn.onClick.AddListener(hide);
    }
    public void updateUI()
    {
        sthAttackCost.text = atkcost + ".0";
        sthDefenceCost.text = defcost + ".0";
        sthHealthCost.text = helcost + ".0";
        if (GameManager.Instance.playerStats.characterStateSo.currCoin < atkcost)
        {
            sthAttackCost.color = Color.red;
            sthAttackBtn.enabled = false;
        }
        else {
            sthAttackCost.color = Color.black;
            sthAttackBtn.enabled = true;

        }
        if (GameManager.Instance.playerStats.characterStateSo.currCoin < defcost)
        {
            sthDefenceCost.color = Color.red;
            sthDefenceBtn.enabled = false;
        }
        else {
            sthDefenceCost.color = Color.black;
            sthDefenceBtn.enabled = true;

        }
        if (GameManager.Instance.playerStats.characterStateSo.currCoin < helcost)
        {
            sthHealthCost.color = Color.red;
            sthHealthBtn.enabled = false;
        }
        else {
            sthHealthCost.color = Color.black;
            sthHealthBtn.enabled = true;
        }
    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
}
