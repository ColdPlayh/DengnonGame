using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingPick: MonoBehaviour
{
    public int add;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject,0.1f);
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.playerStats.characterStateSo.currCoin += add;
        Debug.Log("currCoin" + GameManager.Instance.playerStats.characterStateSo.currCoin);
    }
}
