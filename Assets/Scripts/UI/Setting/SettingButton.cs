using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    Button settingBtn;
    public void Awake()
    {
        settingBtn = this.GetComponent<Button>();
        settingBtn.onClick.AddListener(openSettingMenu);
    }

    public void openSettingMenu()
    {
        SettingManager.Instance.show();
        Time.timeScale = 0;
    }
}
