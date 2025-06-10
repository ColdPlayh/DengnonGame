using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    Animator anim;
    public GameObject coinPrefab;
    public int mincoin;
    public int maxcoin;
    public float maxfallradius;
    public float minfallradius;
    public bool isopen;
    private bool canopen;

    private void Awake()
    {
        isopen = false;
        canopen = false;
       
    }
    
    void Start()
    {
        anim = transform.GetChild(1).GetComponent<Animator>();
        
       
        //Debug.Log("x" + transform.position.x + "y" + transform.position.y + "z" + transform.position.z);
        // Instantiate(coin, new Vector3(transform.position.x+2,4,transform.position.z+1),Quaternion.Euler(90,0,0));
        //Instantiate(coinPrefab, new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z), Quaternion.Euler(90, 0, 0));
    }
    void Update()
    {
        anim.SetBool("isOpen", isopen);
        if (GameManager.Instance.IsSub &&canopen )
        {
            if (isopen)
                return;
            int count = Random.Range(mincoin, maxcoin);
            GameManager.Instance.MaxFallRadius = maxfallradius;
            GameManager.Instance.MinFallRadius = minfallradius;
            isopen = true;
            for (int i = 0; i < count; i++)
            {
                if (coinPrefab != null)
                {
                    Instantiate(coinPrefab,
                   new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z),
                   Quaternion.Euler(90, 0, 0));
                }  
            }
            isopen = true;
        }                    
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canopen = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canopen = false;
        }
    }
    private void OnDestroy()
    {
        SaveManager.Instance.saveChest(isopen.ToString(),gameObject.name);
        SaveManager.Instance.saveChest(canopen.ToString(), gameObject.name + "c");
        
    }
    private void OnEnable()
    {
        string temp = SaveManager.Instance.loadChest(gameObject.name);
        string tempc = SaveManager.Instance.loadChest(gameObject.name + "c");

        isopen = temp != null && bool.Parse(temp);
        canopen = tempc != null && bool.Parse(tempc);
        Debug.Log("save" + " " + isopen + ":" + canopen);
    }
    private void OnDisable()
    {
        
    }


}
