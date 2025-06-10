using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDes : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
