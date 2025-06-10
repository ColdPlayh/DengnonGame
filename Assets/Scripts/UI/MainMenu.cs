using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button newGameBtn;
    Button continueBtn;
    Button quitBtn;
    public Canvas controlCanvans;

    void Awake()
    {
        newGameBtn = transform.GetChild(1).GetComponent<Button>();
        continueBtn = transform.GetChild(2).GetComponent<Button>();
        quitBtn = transform.GetChild(3).GetComponent<Button>();

        quitBtn.onClick.AddListener(quitGame);
        continueBtn.onClick.AddListener(continueGame);
        newGameBtn.onClick.AddListener(newGame);

    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }
    public void newGame()
    {

        PlayerPrefs.DeleteAll();
        GameManager.Instance.GameScore = 0;
        GameManager.Instance.GameTime = 0;
        SceneController.Instance.startGame();
    }

    public void continueGame()
    {
        SceneController.Instance.loadGame();
    }
        

}
