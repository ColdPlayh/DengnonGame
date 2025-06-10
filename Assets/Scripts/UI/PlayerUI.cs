using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : SingleTon<PlayerUI>
{
    Text levelText;
    Text healthText;
    Text coinText;
    Text attackText;
    Text defenceText;
    Text timeText;
    Text scoreText;
    Image healthSlider;
    Image expSlider;


    override protected void Awake()
    {
        base.Awake();
        levelText = transform.GetChild(2).GetComponent<Text>();

        healthText = transform.GetChild(0).GetChild(1).GetComponent<Text>();

        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();

        expSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();

        coinText = transform.GetChild(3).GetChild(0).GetComponent<Text>();

        attackText = transform.GetChild(4).GetChild(0).GetComponent<Text>();

        defenceText = transform.GetChild(5).GetChild(0).GetComponent<Text>();

        scoreText = transform.GetChild(6).GetComponent<Text>();

        timeText = transform.GetChild(7).GetComponent<Text>();
    }

    private void Update()
    {
        updateLevel();
        updateHealth();
        updateExp();
        updateCoin();
        updateAttack();
        updateDfence();
        updateTime();
        updateScore();
        
        
    }
    public void updateLevel()
    {
        levelText.text = "Level " + GameManager.Instance.playerStats.characterStateSo.currLevel;
    }
    public void updateHealth()
    {

        float sliderPercent = (float)GameManager.Instance.playerStats.currHealth / GameManager.Instance.playerStats.maxHealth;
        healthSlider.fillAmount = sliderPercent;
        healthText.text = GameManager.Instance.playerStats.currHealth + "/" + GameManager.Instance.playerStats.maxHealth;
    }
    public void updateExp()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.characterStateSo.currExp
            / GameManager.Instance.playerStats.characterStateSo.baseExp;
        expSlider.fillAmount = sliderPercent;
    }
    public void updateCoin()
    {
        coinText.text = ""+GameManager.Instance.playerStats.characterStateSo.currCoin+".0";
    }
    public void updateAttack()
    {
        attackText.text = "" + GameManager.Instance.playerStats.minDamge+".0";
    }
    public void updateDfence()
    {
        defenceText.text = "" + GameManager.Instance.playerStats.characterStateSo.currDefence+".0";
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
    public void updateScore()
    {
        scoreText.text = "得分:" + GameManager.Instance.GameScore;

    }
    public void updateTime()
    {
        //Debug.Log(GameManager.Instance.TimeChineseFormat(GameManager.Instance.GameTime));
        timeText.text = "时间:" + GameManager.Instance.TimeChineseFormat(GameManager.Instance.GameTime);
    }

}
