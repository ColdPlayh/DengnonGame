using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogNpc : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.IsSub)
        {
            if (GameManager.Instance.tempcd > 0)
                return;
            GameManager.Instance.TempCD = GameManager.Instance.DialogCD;
            Debug.Log("触发对话框");
            DialogManager.Instance.show();
        }
    }
}
