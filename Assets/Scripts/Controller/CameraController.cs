using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingleTon<CameraController>
{
    public float speed;
    public float xoffset;
    public float zoffset;
    private Transform target;
    override protected void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        moveCamera();
    }
    private void moveCamera()
    {
        if (target != null)
        {
            
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(target.transform.position.x+xoffset, transform.position.y, target.transform.position.z+zoffset),
                    speed * Time.deltaTime
                    );
        }
            
    }
    public void setCamera(Vector3 target)
    {
        transform.position = target;
        
    }
    public void changeTarget(Transform newtarget)
    {
        target = newtarget;
    }
}
