using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollAction : MonoBehaviour
{
    public void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    public void onClick()
    {
        Debug.Log(GameManager.Instance);
        GameManager.Instance.playerStats.GetComponent<PlayerController>().roll();
    }
}
