using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinManager : SingleTon<WinManager>
{
    Text timeText;
    Text scoreText;
    Button backMainBtn;
    protected override void Awake()
    {
        base.Awake();
        timeText = transform.GetChild(1).GetComponent<Text>();
        scoreText = transform.GetChild(2).GetComponent<Text>();
        backMainBtn = transform.GetChild(3).GetComponent<Button>();
        backMainBtn.onClick.AddListener(backMain);
    }
    private void OnEnable()
    {
        timeText.text = "用时:" + GameManager.Instance.TimeChineseFormat(GameManager.Instance.GameTime);
        scoreText.text = "得分:" + GameManager.Instance.GameScore;
    }
    private void Start()
    {
        gameObject.SetActive(false);
        
    }
    public void backMain()
    {
        PlayerController.Instance.IsMove = false;
        SceneController.Instance.loadScene("Main");
        
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
