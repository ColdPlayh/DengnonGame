using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierManager : MonoBehaviour
{
    //p0 start  p1 center p2 end
    Vector3 p0, p1, p2, obj;
    [SerializeField, Range(0, 1)] float slider;
    Vector3 result;
    public float fallspeed;
    public float rotationspeed;
    
    private Vector3 center;
    bool ismotion = false;
    // Start is called before the first frame update
   
    void Start()
    {
        result = new Vector3();
       
    }
    public float ran()
    {
        if (Random.value > 0.5)
        {
            return 1;
        }
        else {
            return -1;
        }
    }
    public void createPoint()
    {
        if (ismotion)
            return;
        p0 = transform.position;
        float wayX = UnityEngine.Random.Range(-GameManager.Instance.MaxFallRadius , GameManager.Instance.MaxFallRadius);
        float wayZ = UnityEngine.Random.Range(-GameManager.Instance.MaxFallRadius, GameManager.Instance.MaxFallRadius);
        Debug.Log("x:" + wayX + " y:" + wayZ);
        p2 = new Vector3(p0.x + wayX, transform.position.y+(float)0.5, p0.z + wayZ);
        if (Vector3.Distance(transform.position, p2) > GameManager.Instance.MinFallRadius)
        {
            p1 = new Vector3((p2.x + p0.x) / 2, transform.position.y + 2, (p2.z + p0.z) / 2);
            Debug.Log("p0" + p0 + "p1" + p1 + "p2" + p2);
            ismotion = true;
        }

    }

    private void Update()
    {

        createPoint();
        if (p1 != null)
        {
            if (slider <= 1)
            {
                slider = slider + Time.deltaTime * fallspeed;
                //obj.SetPositionAndRotation(twoPointBezier(p0, p1, p2, slider), .transform.rotation);
                gameObject.transform.position = twoPointBezier(p0, p1, p2, slider);
            }
            else
            {
                gameObject.transform.Rotate(Vector3.forward * rotationspeed);
            }
        }
        

    }
    public Vector3 twoPointBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        
        float t1 = (1 - t) * (1 - t);
        float t2 = 2 * t * (1 - t);
        float t3 = t * t;
        result = p0 * t1 + p1 * t2 + p2 * t3;
        return result;


    }
}
