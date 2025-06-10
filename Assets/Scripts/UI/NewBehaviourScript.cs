using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("1", "diyici");
        PlayerPrefs.Save();
        PlayerPrefs.SetString("2", "dierci");
        PlayerPrefs.Save();
        Debug.Log("ssssmain"+PlayerPrefs.GetString("1") + ":" + PlayerPrefs.GetString("2"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
