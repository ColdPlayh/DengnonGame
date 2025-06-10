using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    { 
        sameScene,differentScene
    }
    public string sceneName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag desitinationTag;
    public bool canTrans;
    private void Update()
    {
        if (canTrans && GameManager.Instance.IsSub)
        {
            
            SceneController.Instance.transitionToDestination(this);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = false;
        }
    }
    
}
