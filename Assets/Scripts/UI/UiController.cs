using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : SingleTon<UiController>
{
    override protected void Awake()
    {
        base.Awake();
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
}
