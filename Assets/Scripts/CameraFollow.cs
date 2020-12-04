using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        moveToOffsetNow();
    }
    public void OnValidate()
    {
        moveToOffsetNow();   
    }

    void moveToOffsetNow()
    {
        if(target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y+height, target.transform.position.z);
        }        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        moveToOffsetNow();
    }
}
