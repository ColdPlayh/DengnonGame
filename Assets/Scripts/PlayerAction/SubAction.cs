using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SubAction : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.subDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.subUp();
    }
}
