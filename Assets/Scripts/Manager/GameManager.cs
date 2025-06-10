using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private bool issub = false;
    public CharacterStats playerStats;
    public Joystick myJoystick;
    public float dialogcd;
    public float tempcd;
    private float maxfallradius;
    private float minfallradius;
    private int gamescore;
    private float gametime;
    private bool isPause;
    //储存所有订阅了gamemanger的对象
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();
    //注册characterstate
    public bool IsPause
    {
        get { return isPause; }
        set { isPause = value; }
    }
    public int GameScore
    {
        get { return gamescore; }
        set { gamescore = value; }
    }
    public float GameTime
    {
        get { return gametime; }
        set { gametime = value; }
    }
    public float DialogCD
    {
        get { return dialogcd; }
    }
    public float MaxFallRadius
    {
        set { maxfallradius = value; }
        get { return maxfallradius; }
    }
    public float MinFallRadius
    {
        set { minfallradius = value; }
        get { return minfallradius; }
    }
    public float TempCD
    {
        get { return tempcd; }
        set { tempcd = value; }
    }
    protected override void Awake()
    {
        tempcd = 0;
        isPause = true;
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void rigisterPlayer(CharacterStats playerStats)
    {
        this.playerStats = playerStats;
    }
    //让其他对象订阅的方法
    public void rigisterObserver(IEndGameObserver rigister)
    {
        if (!endGameObservers.Contains(rigister))
        {
            endGameObservers.Add(rigister);
        }
    }
    public bool containsObserver(IEndGameObserver rigister)
    {
        if (endGameObservers.Contains(rigister))
        {
            return true;
        }
        else
            return false;

    }
    public void rigisterJoyStick(Joystick input)
    {
        this.myJoystick = input;
    }
    //取消订阅的方法
    public void removeObserver(IEndGameObserver remove)
    {
        if (endGameObservers.Contains(remove))
        {
            endGameObservers.Remove(remove);
        }
    }
    //该方法保证凡是注册了gamemanager的对象在发出广播后都会执行自己的endnotify的方法
    public void notifyObserver()
    {
        GameOverManager.Instance.show();

        foreach (var observer in endGameObservers)
        {
            observer.endGameNotify();
        }
        
    }
    public Transform GetEnter()
    {
        foreach (var entance in FindObjectsOfType<TransitionDestination>())
        {
            if (entance.desitinationTag== TransitionDestination.DestinationTag.ENTER)
            {
                return entance.transform;
            }
        }
        return null;
    }
    public bool IsSub
    {
        get { return issub; }
    }
    public void subDown()
    {
        issub = true;
        Debug.Log("按下"+issub);
    }
    public void subUp()
    {
        issub = false;
        Debug.Log("抬起"+issub);
    }
    public void Update()
    {
        updateDialogCd();
        updateTime();
    }
    public void updateDialogCd()
    {
        if (tempcd > 0)
            tempcd -= Time.deltaTime;
    }
    public void updateTime()
    {
        if(!isPause)
            gametime += Time.deltaTime;
        
    }
    public string TimeChineseFormat(float nTotalTime)
    {
        string time = string.Empty;
        float hour = Mathf.Floor(nTotalTime / 3600);
        float min = Mathf.Floor(nTotalTime % 3600 / 60);
        float sec = Mathf.Floor(nTotalTime % 60);
        if (hour > 0)
            time = string.Concat(time, hour, "时");
        if (min > 0)
            time = string.Concat(time, min, "分");
        if (sec > 0)
            time = string.Concat(time, sec, "秒");
        return time;
    }

}
