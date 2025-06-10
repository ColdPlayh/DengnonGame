using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : SingleTon<DialogManager>
{
    [Header("UI组件")]

    public Text textLable;
    public Image faceImage;

    [Header("文本文件")]

    public TextAsset textFile;
    public int index;
    public float textspeed;
    private bool textFinished;
    [Header("头像")]


    List<string> textList = new List<string>();
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        getTextFormFile(textFile);
        textFinished = true;
    }
    void Start()
    {
        Debug.Log("隐藏");
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        StartCoroutine(setTextUI());
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsSub&& index == textList.Count)
        {
            if (GameManager.Instance.tempcd > 0)
                return;
            GameManager.Instance.TempCD = GameManager.Instance.DialogCD;
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
        if (GameManager.Instance.IsSub&&textFinished)
        {
            if (GameManager.Instance.tempcd > 0)
                return;
            GameManager.Instance.TempCD = 0.25f;
            StartCoroutine(setTextUI());
        }
    }
    void getTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var linedata =file.text.Split('\n');
        foreach (var line in linedata)
        {
            textList.Add(line);
        }
    
    }
    IEnumerator setTextUI()
    {
        textFinished = false;
        textLable.text = "";
        
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLable.text += textList[index][i];
            yield return new WaitForSeconds(textspeed);
        }
        index++;
        textFinished = true;
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
