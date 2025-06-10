using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : SingleTon<SettingManager>
{
    Button continueBtn;
    Button backmainBtn;

    protected override void Awake()
    {
        
        base.Awake();
        gameObject.SetActive(false);
        
        continueBtn = transform.GetChild(2).GetComponent<Button>();
        backmainBtn = transform.GetChild(3).GetComponent<Button>();
        continueBtn.onClick.AddListener(continueGame);
        backmainBtn.onClick.AddListener(backMainMenu);
        
    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
    public void continueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void backMainMenu()
    {
        SaveManager.Instance.savePlayerData();
        Time.timeScale = 1;   
        SceneController.Instance.loadScene("Main");
    }

}
