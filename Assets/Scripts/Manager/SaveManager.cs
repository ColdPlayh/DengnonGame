using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : SingleTon<SaveManager>
{

    string sceneName = "";
    public string SceneName { get { return PlayerPrefs.GetString(sceneName); } }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            savePlayerData();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            loadPlayerData();
        }
    }
    public void savePlayerData()
    {
        saveJosn(GameManager.Instance.playerStats.characterStateSo, GameManager.Instance.playerStats.characterStateSo.name);
        saveJosn(GameManager.Instance.playerStats.characterATKStateSo, GameManager.Instance.playerStats.characterATKStateSo.name);
        save();
    }
    public void loadPlayerData()
    {
        loadJosn(GameManager.Instance.playerStats.characterStateSo, GameManager.Instance.playerStats.characterStateSo.name);
        loadJosn(GameManager.Instance.playerStats.characterATKStateSo, GameManager.Instance.playerStats.characterATKStateSo.name);
        load();
    }
    public void saveJosn(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
    public void loadJosn(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
    public void save()
    {
        
        PlayerPrefs.SetInt("score", GameManager.Instance.GameScore);
        PlayerPrefs.SetFloat("time", GameManager.Instance.GameTime);
        PlayerPrefs.SetInt("atkcost", StrengthManager.Instance.atkcost);        
        PlayerPrefs.SetInt("defcost", StrengthManager.Instance.defcost);
        PlayerPrefs.SetInt("helcost", StrengthManager.Instance.helcost);
        PlayerPrefs.SetString(sceneName, SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
    public void saveChest(string data, string key)
    {
        PlayerPrefs.SetString(key, data);       
    }
    public string loadChest(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        return null ;
    }
    public void load()
    {
        Debug.Log("log了");
        GameManager.Instance.GameScore = PlayerPrefs.GetInt("score",0);
        GameManager.Instance.GameTime = PlayerPrefs.GetFloat("time",0f);
        StrengthManager.Instance.atkcost = PlayerPrefs.GetInt("atkcost",25);
        StrengthManager.Instance.defcost = PlayerPrefs.GetInt("defcost",25);         
        StrengthManager.Instance.helcost = PlayerPrefs.GetInt("helcost",25);
        Debug.Log("s"+PlayerPrefs.GetInt("atkcost"));
        Debug.Log("s" + PlayerPrefs.GetInt("defcost"));
        Debug.Log("s" + PlayerPrefs.GetInt("helcost"));
    }
}
