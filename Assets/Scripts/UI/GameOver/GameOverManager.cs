using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverManager: SingleTon<GameOverManager>
{
    Button backmenuBtn;
    Button restartBtn;
    override protected void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
        restartBtn = transform.GetChild(1).GetComponent<Button>();
        backmenuBtn = transform.GetChild(2).GetComponent<Button>();
        backmenuBtn.onClick.AddListener(backMainMenu);
        restartBtn.onClick.AddListener(reStart);


    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
    public void backMainMenu()
    {
        SceneController.Instance.loadScene("Main");
    }
    public void reStart()
    {
        SceneController.Instance.loadScene(SceneManager.GetActiveScene().name);
    }
}
