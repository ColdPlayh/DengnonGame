using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthTable : MonoBehaviour
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
            StrengthManager.Instance.show();
        }
    }
}
