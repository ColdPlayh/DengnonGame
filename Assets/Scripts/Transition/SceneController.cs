using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class SceneController : SingleTon<SceneController>
{
    public GameObject playerpreFab;
    private GameObject player;
    public Canvas controlCanvas;
    public Canvas statsCanvas;
    public Canvas dialogCanvas;
    public Canvas strengthCanvas;
    public Canvas pauseCanvas;
    public Canvas gameoverCanvas;
    public Canvas victoryCanvas;
    public Camera mainCamera;
    [Header("LoadSlider")]
    public GameObject loadCanvs;
    public Slider loadSlider;
    public Text loadText;
   

  
    protected override void Awake()
    {
        base.Awake();
        loadCanvs.SetActive(false);
        //DontDestroyOnLoad(this);
    }
    public void transitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.sameScene:
                StartCoroutine(transition(SceneManager.GetActiveScene().name, transitionPoint.desitinationTag));
                break;
            case TransitionPoint.TransitionType.differentScene:
                GameManager.Instance.IsPause = true;
                StartCoroutine(transition(transitionPoint.sceneName, transitionPoint.desitinationTag));
                GameManager.Instance.IsPause = false;
                break;
        }
    }
    IEnumerator transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        Debug.Log(sceneName);
        SaveManager.Instance.savePlayerData();
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            GameManager.Instance.subUp();
            loadCanvs.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                loadSlider.value = operation.progress;
                loadText.text = "加载中:"+operation.progress * 100 + "%";
                if (operation.progress >= 0.9f)
                {
                    loadSlider.value = 1;
                    loadText.text = "触摸屏幕继续";
                    if (Input.touchCount > 0 ||Input.anyKeyDown)
                    {
                        operation.allowSceneActivation = true;
                        
                    }
                    yield return null;
                }

            }
            Instantiate(mainCamera);
            CameraController.Instance.setCamera(new Vector3((float)-0.09315109, 16, (float)-0.2619858));            
            Instantiate(controlCanvas);
            Instantiate(dialogCanvas);
            Instantiate(statsCanvas);
            Instantiate(strengthCanvas);
            Instantiate(pauseCanvas);
            Instantiate(gameoverCanvas);
            Instantiate(victoryCanvas);
            Instantiate(playerpreFab, getDestination(destinationTag).transform.position,
             getDestination(destinationTag).transform.rotation);
            SaveManager.Instance.loadPlayerData();
            loadCanvs.SetActive(false);
            yield break;
        }
        else {
            player = GameManager.Instance.playerStats.gameObject;
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.SetPositionAndRotation(getDestination(destinationTag).transform.position,
               player.transform.rotation);
            player.GetComponent<NavMeshAgent>().enabled = true;
            //player.transform.position = getDestination(destinationTag).transform.position;
            yield return null;

        }
        
    
    }
    public void loadGame()
    {
        
        StartCoroutine(enterlevel(SaveManager.Instance.SceneName));
    }
    public void loadScene(string name)
    {
        StartCoroutine(enterlevel(name));
       
    }
    public void startGame()
    {

        StartCoroutine(enterlevel("Game1"));
    }
    IEnumerator enterlevel(string name)
    {
        if (name != "" && name != "Main")
        {
            GameManager.Instance.IsPause = true;
            loadCanvs.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(name);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                loadSlider.value = operation.progress;
                loadText.text = "加载中:" + operation.progress * 100 + "%";
                if (operation.progress >= 0.9f)
                {
                    loadSlider.value = 1;
                    loadText.text = "触摸屏幕继续";
                    if (Input.touchCount > 0||Input.anyKeyDown)
                    {
                        
                        operation.allowSceneActivation = true;

                        

                    }
                    yield return null;

                }
                
            }
            
           Instantiate(mainCamera);
           
            if (name == "Game2")
           {
               CameraController.Instance.setCamera(new Vector3((float)-0.09315109, 16, (float)-0.2619858));
           }
           Instantiate(controlCanvas);
           Instantiate(dialogCanvas);
           Instantiate(statsCanvas);
           Instantiate(strengthCanvas);
           Instantiate(pauseCanvas);
           Instantiate(gameoverCanvas);
           Instantiate(victoryCanvas);
           Instantiate(playerpreFab,
                GameManager.Instance.GetEnter().position,
                GameManager.Instance.GetEnter().rotation);
           loadCanvs.SetActive(false);
           GameManager.Instance.IsPause = false;
           
          /* yield return Instantiate(mainCamera);
           if (name == "Game2")
           {
               CameraController.Instance.setCamera(new Vector3((float)-0.09315109, 16, (float)-0.2619858));
           }
           yield return Instantiate(controlCanvas);
           yield return Instantiate(dialogCanvas);
           yield return Instantiate(statsCanvas);
           yield return Instantiate(strengthCanvas);
           yield return Instantiate(pauseCanvas);
           yield return Instantiate(gameoverCanvas);
           yield return Instantiate(victoryCanvas);
           yield return Instantiate(playerpreFab,
                GameManager.Instance.GetEnter().position,
                GameManager.Instance.GetEnter().rotation);
           GameManager.Instance.IsPause = false;
          */

        
        }
        else if (name == "Main")
        {            
            GameManager.Instance.IsPause = true;
            yield return SceneManager.LoadSceneAsync(name);
            
           // Destroy(GameManager.Instance.playerStats.gameObject);
        }
        else {
            Debug.Log("不合法的场景");
        }
        yield break;
    }

    private TransitionDestination getDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].desitinationTag == destinationTag)
            {
                return entrances[i];
            }
        }
        return null;
    }
}
